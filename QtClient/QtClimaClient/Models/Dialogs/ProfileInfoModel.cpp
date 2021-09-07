#include "ProfileInfoModel.h"

ProfileInfoModel::ProfileInfoModel(QList<ProfileInfo> *infos, QObject *parent)
    : QAbstractTableModel(parent)
{
    m_infos = infos;
}

QVariant ProfileInfoModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role != Qt::DisplayRole)
        return QVariant();

    if(orientation == Qt::Horizontal)
    {
        if(section == 0)
            return "Имя";
        else if(section == 1)
            return "Описание";
    }
    else if(orientation == Qt::Vertical)
    {
        return QString::number(section);
    }

    return QVariant();
}


int ProfileInfoModel::rowCount(const QModelIndex &parent) const
{
    return m_infos->count();
}

int ProfileInfoModel::columnCount(const QModelIndex &parent) const
{
    return 2;
}

QVariant ProfileInfoModel::data(const QModelIndex &index, int role) const
{
    if(role == Qt::DisplayRole)
        switch(index.column())
        {
        case 0:
            return m_infos->at(index.row()).Name;
            break;
        case 1:
            return m_infos->at(index.row()).Description;
            break;
        }
    return QVariant();
}

QString ProfileInfoModel::getProfileKey(int rowNumber)
{
    return m_infos->at(rowNumber).Key;
}

int ProfileInfoModel::getRowNumber(const QString &key)
{
    for(int i = 0; i < m_infos->count(); i++)
    {
        if(m_infos->at(i).Key == key)
            return i;
    }
    return -1;
}

