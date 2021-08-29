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
    }

    return QVariant();
}

bool TempProfileModel::setHeaderData(int section, Qt::Orientation orientation, const QVariant &value, int role)
{
    if (value != headerData(section, orientation, role)) {
        // FIXME: Implement me!
        emit headerDataChanged(orientation, section, section);
        return true;
    }
    return false;
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

bool TempProfileModel::setData(const QModelIndex &index, const QVariant &value, int role)
{
    if (data(index, role) != value) {
        // FIXME: Implement me!
        emit dataChanged(index, index, QVector<int>() << role);
        return true;
    }
    return false;
}

Qt::ItemFlags TempProfileModel::flags(const QModelIndex &index) const
{
    if (!index.isValid())
        return Qt::NoItemFlags;

    return Qt::ItemIsEditable; // FIXME: Implement me!
}

bool TempProfileModel::insertColumns(int column, int count, const QModelIndex &parent)
{
    beginInsertColumns(parent, column, column + count - 1);
    // FIXME: Implement me!
    endInsertColumns();
    return true;
}

bool TempProfileModel::removeColumns(int column, int count, const QModelIndex &parent)
{
    beginRemoveColumns(parent, column, column + count - 1);
    // FIXME: Implement me!
    endRemoveColumns();
    return true;
}
