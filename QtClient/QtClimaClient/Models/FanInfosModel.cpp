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
                return "произв-сть\nвсего";
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
    return m_infos->count();
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
                QString modeChar;
                QString stateChar;
                if(m_infos->at(index.row()).Mode == (int)FanModeEnum::Auto)
                {
                    modeChar = 'A';
                }
                else if(m_infos->at(index.row()).Mode == (int)FanModeEnum::Manual)
                {
                    modeChar = 'M';
                }
                else if(m_infos->at(index.row()).Mode == (int)FanModeEnum::Disabled)
                {
                    modeChar = 'D';
                }

                if(m_infos->at(index.row()).State == (int)FanStateEnum::Running)
                {
                    stateChar = 'R';
                }
                else if(m_infos->at(index.row()).State == (int)FanStateEnum::Stopped)
                {
                    stateChar = 'S';
                }
                else if(m_infos->at(index.row()).State == (int)FanStateEnum::Alarm)
                {
                    stateChar = 'E';
                }
                if(m_infos->at(index.row()).IsAnalog)
                {
                    return modeChar + stateChar + ' ' + QString::number(m_infos->at(index.row()).AnalogPower, 'f', 1) + '%';
                }
                else
                {
                    return modeChar + stateChar;
                }
            }
            break;
            case 1:
            return m_infos->at(index.row()).FanName;
            break;
            case 2:
            return m_infos->at(index.row()).FanCount;
            break;
            case 3:
            return m_infos->at(index.row()).Performance * m_infos->at(index.row()).FanCount;
            break;
            case 4:
            return m_infos->at(index.row()).Priority;
            break;
            case 5:
            return m_infos->at(index.row()).Hermetised;
            break;

        }
    }
    if(role == Qt::BackgroundRole)
    {
        if(index.column() == 0)
        {
            if(m_infos->at(index.row()).State == (int)FanStateEnum::Running)
            {
                return QColor(Qt::green);
            }
            else if(m_infos->at(index.row()).State == (int)FanStateEnum::Stopped)
            {
                return QColor(Qt::white);
            }
            else if(m_infos->at(index.row()).State == (int)FanStateEnum::Alarm)
            {
                return QColor("lightred");
            }
        }
    }

    return QVariant();
}

void FanInfosModel::setFanInfoList(const QList<FanInfo> *infos)
{
    m_infos = infos;
    emit dataChanged(index(0, 0), index(rowCount()-1, columnCount()-1));
}


