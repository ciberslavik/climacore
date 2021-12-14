#include "TimerProfileModel.h"

TimerProfileModel::TimerProfileModel(LightTimerProfile *profile, QObject *parent)
    : QAbstractTableModel(parent),
      m_profile(profile)
{
    m_rowCount = m_profile->getRowCount();
}


QVariant TimerProfileModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::DisplayRole)
    {
        if(orientation == Qt::Horizontal)
        {
            if(section > m_profile->Days.count()-1)
                return QVariant();

            return QString::number(m_profile->Days[section].DayNumber);
        }
    }
    return QVariant();
}

int TimerProfileModel::rowCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;
    int rows = m_profile->getRowCount();

    return rows;
}

int TimerProfileModel::columnCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return m_profile->Days.count();
}

QVariant TimerProfileModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if(role == Qt::DisplayRole)
    {
        if(index.column()<m_profile->Days.count())
        {
            if(index.row()<m_profile->Days[index.column()].Timers.count())
            {
                LightTimerItem ti = m_profile->Days[index.column()].Timers[index.row()];
                return ti.OnTime.toString("hh:mm") + " " + ti.OffTime.toString("hh:mm");
            }
        }
    }
    // FIXME: Implement me!
    return QVariant();
}

void TimerProfileModel::DataUpdated()
{
    beginInsertColumns(QModelIndex(),columnCount()-1,columnCount()-1);
    endInsertColumns();

    if(m_rowCount > m_profile->getRowCount())
    {
        beginInsertRows(QModelIndex(),rowCount()-1,rowCount()-1);
        endInsertRows();
    }
    emit dataChanged(index(0, 0), index(rowCount() - 1, columnCount() - 1));
}
