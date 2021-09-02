#pragma once

#include <QAbstractTableModel>
#include <Models/FanInfo.h>

class FanInfosModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit FanInfosModel(QList<FanInfo> *infos, QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;


    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

private:
    QList<FanInfo> *m_infos;
};

