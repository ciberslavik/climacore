#include "FanInfosModel.h"

FanInfosModel::FanInfosModel(QObject *parent)
    : QAbstractTableModel(parent)
{

}

QVariant FanInfosModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role == Qt::DisplayRole)
    {
        if(orientation == Qt::Horizontal)
        {
            switch (section) {
                case 0:
                return "Сост";
                case 1:
                return "Имя";
                case 2:
                return "колл-во";
                case 3:
                return "произв-сть всего";
                case 4:
                return "Приоритет";
                case 5:
                return "Герм.";
            }
        }
    }
    return QVariant();
}


int FanInfosModel::rowCount(const QModelIndex &parent) const
{
    Q_UNUSED(parent)

    return m_infos.count();
}

int FanInfosModel::columnCount(const QModelIndex &parent) const
{
    Q_UNUSED(parent)
    return 6;
}

QVariant FanInfosModel::data(const QModelIndex &index, int role) const
{
    if(role == Qt::DisplayRole)
    {
        switch (index.column()) {
            case 0:
            {
                return getModeString(m_infos[index.row()]);
            }
            break;
            case 1:
            return m_infos[index.row()].FanName;
            break;
            case 2:
            return m_infos[index.row()].FanCount;
            break;
            case 3:
            return m_infos[index.row()].Performance * m_infos[index.row()].FanCount;
            break;
            case 4:
            return m_infos[index.row()].Priority;
            break;
            case 5:
            return m_infos[index.row()].Hermetised;
            break;

        }
    }
    if(role == Qt::BackgroundRole)
    {
        if(index.column() == 0)
        {
            return getModeColor(m_infos[index.row()]);
        }
    }

    return QVariant();
}

void FanInfosModel::setFanInfoList(const QList<FanInfo> &infos)
{
    beginInsertRows(QModelIndex(), 0, infos.count() - 1);

    m_infos = infos;

    qSort(m_infos.begin(),m_infos.end(),
          [](const FanInfo &a, const FanInfo &b) -> bool { return a.Priority < b.Priority; });

    endInsertRows();
}

void FanInfosModel::updateFanInfoList(const QList<FanInfo> &infos)
{
    m_infos = infos;

    qSort(m_infos.begin(),m_infos.end(),
          [](const FanInfo &a, const FanInfo &b) -> bool { return a.Priority < b.Priority; });

    beginResetModel();
    emit dataChanged(index(0, 0), index(rowCount()-1, 0), {Qt::DisplayRole});
}

QString FanInfosModel::getRowKey(int row)
{
    return m_infos[row].Key;
}

QColor FanInfosModel::getModeColor(const FanInfo &info) const
{
    if(info.IsAlarm)
    {
        return QColor("lightred");
    }

    if(info.State == (int)FanModeEnum::Disabled)
        return QColor(Qt::lightGray);

    if(info.State == (int)FanStateEnum::Running)
    {
        return QColor(Qt::green);
    }
    else if(info.State == (int)FanStateEnum::Stopped)
    {
        return QColor(Qt::white);
    }
    else
        return QColor(Qt::white);
}

QString FanInfosModel::getModeString(const FanInfo &info) const
{
    QString modeChar;
    QString stateChar;

    if(info.Mode == (int)FanModeEnum::Auto)
    {
        modeChar = 'A';
    }
    else if(info.Mode == (int)FanModeEnum::Manual)
    {
        modeChar = 'M';
    }
    else if(info.Mode == (int)FanModeEnum::Disabled)
    {
        modeChar = 'D';
    }
    if(info.IsAlarm)
    {
        stateChar = "Err";
    }
    else
    {
        if(info.State == (int)FanStateEnum::Running)
        {
            stateChar = 'R';
        }
        else if(info.State == (int)FanStateEnum::Stopped)
        {
            stateChar = 'S';
        }
    }
    if(info.IsAnalog)
    {
        return modeChar + " " + stateChar + ' ' + QString::number(info.AnalogPower, 'f', 1) + '%';
    }
    else
    {
        return modeChar + " " + stateChar;
    }
}


