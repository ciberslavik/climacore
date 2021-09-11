#include "VentProfileModel.h"

VentProfileModel::VentProfileModel(MinMaxByDayProfile *profile, QObject *parent)
    : QAbstractTableModel(parent)
{
    m_profile = profile;
}

QVariant VentProfileModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::ItemDataRole::DisplayRole)
    {
        if(orientation == Qt::Orientation::Horizontal)
            return m_profile->Points.at(section).Day;
        else if(orientation == Qt::Orientation::Vertical)
        {
            if(section == 0)
                return "Max";
            else if(section == 1)
                return "Min";
        }
    }

    return QVariant();
}



int VentProfileModel::rowCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return 2;
}

int VentProfileModel::columnCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;
qDebug() << "points:" << m_profile->Points.count();
    return m_profile->Points.count();
}

QVariant VentProfileModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if(role == Qt::DisplayRole)
    {
        if(index.row()==0)
            return m_profile->Points.at(index.column()).MaxValue;
        else if(index.row()==1)
            return m_profile->Points.at(index.column()).MinValue;

    }

    return QVariant();
}

bool VentProfileModel::removeColumns(int column, int count, const QModelIndex &parent)
{
    Q_UNUSED(count)

    beginRemoveColumns(parent, column, column);
    m_profile->Points.removeAt(column);
    endRemoveColumns();
    emit layoutChanged();
    return true;
}

void VentProfileModel::removePoint(const int &index)
{
    removeColumns(index, 1, QModelIndex());
}


