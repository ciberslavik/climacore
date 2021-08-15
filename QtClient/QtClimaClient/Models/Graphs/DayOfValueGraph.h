#pragma once

#include "GraphBase.h"
#include <QObject>

class DayOfValueGraph : public GraphBase
{
    Q_OBJECT
public:
    explicit DayOfValueGraph(QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;

    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;
};

