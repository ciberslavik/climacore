#pragma once

#include <QAbstractTableModel>
#include <Models/Graphs/MinMaxByDayProfile.h>

class VentProfileModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit VentProfileModel(MinMaxByDayProfile *profile, QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;


    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

    MinMaxByDayProfile *getProfile(){return m_profile;}
private:

    MinMaxByDayProfile *m_profile;

    // QAbstractItemModel interface
public:
    bool removeColumns(int column, int count, const QModelIndex &parent) override;

    void removePoint(const int &index);
};

