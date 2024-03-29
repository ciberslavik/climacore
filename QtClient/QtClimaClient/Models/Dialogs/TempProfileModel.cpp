#include "TempProfileModel.h"

TempProfileModel::TempProfileModel(ValueByDayProfile *profile, QObject *parent)
    : QAbstractTableModel(parent)
{
    m_profile = profile;
}

QVariant TempProfileModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::ItemDataRole::DisplayRole)
    {
        if(orientation == Qt::Orientation::Horizontal)
            return m_profile->Points.at(section).Day;
        else if(orientation == Qt::Orientation::Vertical)
            return "Temp";
    }

    return QVariant();
}



int TempProfileModel::rowCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return 1;
}

int TempProfileModel::columnCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return m_profile->Points.count();
}

QVariant TempProfileModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if(role == Qt::DisplayRole)
    {
        if(index.row()==0)
            return m_profile->Points.at(index.column()).Value;

    }

    return QVariant();
}
