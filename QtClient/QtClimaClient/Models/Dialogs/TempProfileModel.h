#pragma once

#include <QAbstractTableModel>
#include <Models/Graphs/ValueByDayProfile.h>

class TempProfileModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit TempProfileModel(ValueByDayProfile *profile, QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;


    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

    ValueByDayProfile *getProfile(){return m_profile;}
private:

    ValueByDayProfile *m_profile;
};

