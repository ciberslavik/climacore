#pragma once

#include <QAbstractTableModel>
#include <Models/Graphs/ValueByValueProfile.h>

class ValveProfileModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit ValveProfileModel(ValueByValueProfile *profile, QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;


    // Basic functionality:
    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

    ValueByValueProfile *getProfile(){return m_profile;}
private:

    ValueByValueProfile *m_profile;
};

