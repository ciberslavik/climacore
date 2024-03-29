#include "TimerProfileModel.h"

TimerProfileModel::TimerProfileModel(LightTimerProfile *profile, QObject *parent)
    : QAbstractTableModel(parent),
      m_profile(profile)
{
    m_rowCount = m_profile->getRowCount();
    m_colCount = m_profile->Days.count();
    for(int i = 0; i < m_profile->Days.count(); i++)
    {
        LightTimerDay *day = &m_profile->Days[i];

        qSort(day->Timers.begin(),day->Timers.end(),[]
              (const LightTimerItem &a, const LightTimerItem &b){
            return a.OnTime < b.OnTime;
        });
    }
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
            LightTimerDay *day = &m_profile->Days[index.column()];


            if(index.row() < day->Timers.count())
            {
                LightTimerItem ti = day->Timers[index.row()];

                return ti.OnTime.toString("hh:mm") + " " + ti.OffTime.toString("hh:mm");
            }
        }
    }
    // FIXME: Implement me!
    return QVariant();
}

void TimerProfileModel::DataUpdated()
{
    for(int i = 0; i < m_profile->Days.count(); i++)
    {
        LightTimerDay *day = &m_profile->Days[i];

        qSort(day->Timers.begin(),day->Timers.end(),[]
              (const LightTimerItem &a, const LightTimerItem &b){
            return a.OnTime < b.OnTime;
        });
    }

    if(m_colCount < m_profile->Days.count())
    {
        m_colCount = m_profile->Days.count();
        beginInsertColumns(QModelIndex(),columnCount()-1, columnCount()-1);
        endInsertColumns();
    }
    if(m_rowCount < m_profile->getRowCount())
    {
        m_rowCount = m_profile->getRowCount();
        beginInsertRows(QModelIndex(),rowCount()-1,rowCount()-1);
        endInsertRows();
    }

    emit dataChanged(index(0, 0), index(rowCount() - 1, columnCount() - 1));
}
