#include "IOOverviewModel.h"

IOOverviewModel::IOOverviewModel(QObject *parent)
    : QAbstractTableModel(parent)
{
}

QVariant IOOverviewModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::ItemDataRole::DisplayRole)
    {
        if(orientation == Qt::Horizontal)
        {
            switch (section) {
                case 0:
                return QString("Pin name");

                case 1:
                return QString("State");
            }
        }
    }
    return QVariant();
}

int IOOverviewModel::rowCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return m_infos.count();
    // FIXME: Implement me!
}

int IOOverviewModel::columnCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return 2;
    // FIXME: Implement me!
}

QVariant IOOverviewModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();
    if(role == Qt::DisplayRole)
    {
        if(index.column()==0)
        {
            return m_infos[index.row()].PinName;
        }
        else if(index.column()==1)
        {
            return m_infos[index.row()].PinState;
        }
    }
    // FIXME: Implement me!
    return QVariant();
}

void IOOverviewModel::setPinInfos(const QList<PinInfo> &infos)
{
    m_infos = infos;

}
