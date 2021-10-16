#pragma once

#include <QAbstractTableModel>
#include <Models/FanInfo.h>
#include "Models/FanControlsEnums.h"
#include <QColor>

class FanInfosModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit FanInfosModel(QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;


    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;
    void setFanInfoList(const QList<FanInfo> *infos);
    QList<FanInfo> *getFanInfoList()const;
private:
    const QList<FanInfo> *m_infos ;

};

