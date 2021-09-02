#include "FanInfosModel.h"

FanInfosModel::FanInfosModel(QList<FanInfo> *infos, QObject *parent)
    : QAbstractTableModel(parent)
{
    m_infos = infos;
}

QVariant FanInfosModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::DisplayRole)
    {
        if(orientation == Qt::Horizontal)
        {
            switch (section) {
            case 0:
                return "Имя";
            case 1:
                return "произв-сть\n на вент.";
            case 2:
                return "колл-во";
            case 3:
                return "произв-сть\nвсего";
            case 4:
                return "Приоритет";
            case 5:
                return "Герм.";
            }
        }
    }
    return QVariant();
}


int FanInfosModel::rowCount(const QModelIndex &parent) const
{
    if (!parent.isValid())
        return 0;

    return m_infos->count();
}

int FanInfosModel::columnCount(const QModelIndex &parent) const
{
    if (!parent.isValid())
        return 0;

    return 5;
}

QVariant FanInfosModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if(role == Qt::DisplayRole)
    {
        switch (index.column()) {
        case 0:
            return m_infos->at(index.row()).FanName;
            break;
        case 1:
            return m_infos->at(index.row()).Performance;
            break;
        case 2:
            return m_infos->at(index.row()).FanCount;
            break;
        case 3:
            return m_infos->at(index.row()).Performance * m_infos->at(index.row()).FanCount;
            break;
        case 4:
            return m_infos->at(index.row()).Priority;
            break;
        case 5:
            return m_infos->at(index.row()).Hermetise;
            break;

        }
    }
    return QVariant();
}
