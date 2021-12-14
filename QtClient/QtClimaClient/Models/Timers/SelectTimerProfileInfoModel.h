#pragma once

#include <QAbstractTableModel>
#include "Models/Timers/LightTimerProfile.h"
#include "LightTimerProfileInfo.h"

class SelectTimerProfileInfoModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit SelectTimerProfileInfoModel(QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;

    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

    void setProfileList(QList<LightTimerProfileInfo> *profiles);

private:

    QList<LightTimerProfileInfo> *m_profiles;
};

