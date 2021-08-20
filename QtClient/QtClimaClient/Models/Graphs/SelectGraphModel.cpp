#include "SelectGraphModel.h"

SelectGraphModel::SelectGraphModel(QList<GraphInfo> infos, QObject *parent)
    : QAbstractItemModel(parent),
      m_infos(infos)
{
}

QVariant SelectGraphModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    Q_UNUSED(orientation)

    if(role == Qt::DisplayRole)
    {
        switch (section) {
        case 0:
            return "Key";
            break;
        case 1:
            return "Имя";
            break;
        case 2:
            return "Описание";
        }
    }
}

QModelIndex SelectGraphModel::index(int row, int column, const QModelIndex &parent) const
{
    // FIXME: Implement me!
}

QModelIndex SelectGraphModel::parent(const QModelIndex &index) const
{
    // FIXME: Implement me!
}

int SelectGraphModel::rowCount(const QModelIndex &parent) const
{
    if (!parent.isValid())
        return 0;

    return m_infos.count();
}

int SelectGraphModel::columnCount(const QModelIndex &parent) const
{
    if (!parent.isValid())
        return 0;

    return 3;
}

QVariant SelectGraphModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    // FIXME: Implement me!
    return QVariant();
}
