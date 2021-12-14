#pragma once

#include <QAbstractTableModel>

#include <Models/PinInfo.h>

class IOOverviewModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit IOOverviewModel(QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;

    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;
    void setPinInfos(const QList<PinInfo> &infos);
private:
    QList<PinInfo> m_infos;
};

