#pragma once

#include <QAbstractTableModel>
#include <Models/Graphs/ProfileInfo.h>

class ProfileInfoModel : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit ProfileInfoModel(QList<ProfileInfo> *infos, QObject *parent = nullptr);

    // Header:
    QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;


    int rowCount(const QModelIndex &parent = QModelIndex()) const override;
    int columnCount(const QModelIndex &parent = QModelIndex()) const override;

    QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

    QList<ProfileInfo> *infos(){return  m_infos;}
    int getRowNumber(const QString &key);
private:
    QList<ProfileInfo> *m_infos;

};

