#pragma once

#include <QAbstractTableModel>
#include "Models/LivestockOperation.h"

class LivestockOperationsModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit LivestockOperationsModel(QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;

    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

    void setLivestockOperations(const QList<LivestockOperation> &operations);
    QString getOperationType(const int &opType) const;


private:

    QList<LivestockOperation> m_operations;
};

