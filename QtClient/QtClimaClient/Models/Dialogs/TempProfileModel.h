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

    bool setHeaderData(int section, Qt::Orientation orientation, const QVariant &value, int role = Qt::EditRole) override;

    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

    // Editable:
    bool setData(const QModelIndex &index, const QVariant &value,
                 int role = Qt::EditRole) override;

    Qt::ItemFlags flags(const QModelIndex& index) const override;

    // Add data:
    bool insertColumns(int column, int count, const QModelIndex &parent = QModelIndex()) override;

    // Remove data:
    bool removeColumns(int column, int count, const QModelIndex &parent = QModelIndex()) override;


    ValueByDayProfile *getProfile(){return m_profile;}
private:

    ValueByDayProfile *m_profile;
};

