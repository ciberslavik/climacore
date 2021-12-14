#include "LivestockOperationsModel.h"

LivestockOperationsModel::LivestockOperationsModel(QObject *parent)
    : QAbstractTableModel(parent)
{
}

QVariant LivestockOperationsModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::DisplayRole)
    {
        if(orientation == Qt::Horizontal)
        {
            switch (section) {
                case 0:
                return "Операция";
                case 1:
                return "Кол-во голов";
                case 2:
                return "Дата операции";
                default:
                return QVariant();
            }
        }
    }
    return QVariant();
}

int LivestockOperationsModel::rowCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;

    return m_operations.count();
    // FIXME: Implement me!
}

int LivestockOperationsModel::columnCount(const QModelIndex &parent) const
{
    if (parent.isValid())
        return 0;
    return 3;
    // FIXME: Implement me!
}

QVariant LivestockOperationsModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if(role == Qt::DisplayRole)
    {
        switch (index.column()) {
            case 0:
                return getOperationType(m_operations[index.row()].OperationType);
            case 1:
                return m_operations[index.row()].HeadCount;
            case 2:
                return m_operations[index.row()].OperationDate;
            default:
                return QVariant();
        }
    }
    // FIXME: Implement me!
    return QVariant();
}

void LivestockOperationsModel::setLivestockOperations(const QList<LivestockOperation> &operations)
{
    m_operations = operations;
}

QString LivestockOperationsModel::getOperationType(const int &opType) const
{
    QString result = "Unknown";
    switch (opType) {
        case 0:
            result = "Посадка";
        break;
        case 1:
            result = "Убой";
        break;
        case 2:
            result = "Падеж";
        break;
        case 3:
            result = "Разрежение";
        break;

    }
    return result;
}
