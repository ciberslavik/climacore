#include "FanInfosModel.h"

FanInfosModel::FanInfosModel(QObject *parent)
    : QAbstractTableModel(parent)
{

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
    Q_UNUSED(parent)
    return m_infos->count();
}

int FanInfosModel::columnCount(const QModelIndex &parent) const
{
    Q_UNUSED(parent)
    return 6;
}

QVariant FanInfosModel::data(const QModelIndex &index, int role) const
{
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
            return m_infos->at(index.row()).Hermetised;
            break;

        }
    }
    return QVariant();
}

void FanInfosModel::setFanInfoList(const QList<FanInfo> *infos)
{
    m_infos = infos;
    QModelIndex topLeft = index(0, 0);
    QModelIndex bottomRight = index(m_infos->count()-1, 5);
    emit dataChanged(topLeft, bottomRight, {Qt::DisplayRole});
}


