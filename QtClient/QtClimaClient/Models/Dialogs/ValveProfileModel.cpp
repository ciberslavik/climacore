#include "ValveProfileModel.h"

ValveProfileModel::ValveProfileModel(ValueByValueProfile *profile, QObject *parent)
    : QAbstractTableModel(parent)
{
    m_profile = profile;
}

QVariant ValveProfileModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::ItemDataRole::DisplayRole)
    {
        if(orientation == Qt::Orientation::Horizontal)
            return m_profile->Points.at(section).ValueX;
        else if(orientation == Qt::Orientation::Vertical)
            return "Клап.";
    }

    return QVariant();
}



int ValveProfileModel::rowCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return 1;
}

int ValveProfileModel::columnCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return m_profile->Points.count();
}

QVariant ValveProfileModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if(role == Qt::DisplayRole)
    {
        if(index.row()==0)
            return m_profile->Points.at(index.column()).ValueY;

    }

    return QVariant();
}
