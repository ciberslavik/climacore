#include "SelectTimerProfileInfoModel.h"

SelectTimerProfileInfoModel::SelectTimerProfileInfoModel(QObject *parent)
    : QAbstractTableModel(parent)
{
}

QVariant SelectTimerProfileInfoModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::DisplayRole)
        if(orientation == Qt::Horizontal)
        {
            switch (section) {
                case 0:
                return "Имя профиля";
                case 1:
                return "Описание";
            }
        }

    return QVariant();
    // FIXME: Implement me!
}

int SelectTimerProfileInfoModel::rowCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;
    return m_profiles->count();
    // FIXME: Implement me!
}

int SelectTimerProfileInfoModel::columnCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;
    return 2;
    // FIXME: Implement me!
}

QVariant SelectTimerProfileInfoModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if(role == Qt::DisplayRole)
    {
        switch (index.column()) {
            case 0:
                return m_profiles->at(index.row()).Name;
            case 1:
                return m_profiles->at(index.row()).Description;

        }
    }
    return QVariant();
}

void SelectTimerProfileInfoModel::setProfileList(QList<LightTimerProfileInfo> *profiles)
{
    m_profiles = profiles;
}


