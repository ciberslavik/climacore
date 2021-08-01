#pragma once

#include <QObject>

class SystemState : public QObject
{
    Q_OBJECT
    Q_PROPERTY(float FrontTemperature READ FrontTemperature WRITE setFrontTemperature NOTIFY FrontTemperatureChanged)
    Q_PROPERTY(float RearTemperature READ RearTemperature WRITE setRearTemperature NOTIFY RearTemperatureChanged)
    Q_PROPERTY(float OutdoorTemperature READ OutdoorTemperature WRITE setOutdoorTemperature NOTIFY OutdoorTemperatureChanged)
    Q_PROPERTY(float Humidity READ Humidity WRITE setHumidity NOTIFY HumidityChanged)
    Q_PROPERTY(float Presure READ Presure WRITE setPresure NOTIFY PresureChanged)

    float m_FrontTemperature;

    float m_RearTemperature;

    float m_OutdoorTemperature;

    float m_Humidity;

    float m_Presure;

public:
    SystemState()
    {

    }
    float FrontTemperature() const
    {
        return m_FrontTemperature;
    }

    float RearTemperature() const
    {
        return m_RearTemperature;
    }

    float OutdoorTemperature() const
    {
        return m_OutdoorTemperature;
    }

    float Humidity() const
    {
        return m_Humidity;
    }

    float Presure() const
    {
        return m_Presure;
    }

    void InvokeUpdate()
    {
        emit StateUpdate();
    }
public slots:
    void setFrontTemperature(float FrontTemperature)
    {
        qWarning("Floating point comparison needs context sanity check");
        if (qFuzzyCompare(m_FrontTemperature, FrontTemperature))
            return;

        m_FrontTemperature = FrontTemperature;
        emit FrontTemperatureChanged(m_FrontTemperature);
        emit StateUpdate();
    }

    void setRearTemperature(float RearTemperature)
    {
        qWarning("Floating point comparison needs context sanity check");
        if (qFuzzyCompare(m_RearTemperature, RearTemperature))
            return;

        m_RearTemperature = RearTemperature;
        emit RearTemperatureChanged(m_RearTemperature);
        emit StateUpdate();
    }

    void setOutdoorTemperature(float OutdoorTemperature)
    {
        qWarning("Floating point comparison needs context sanity check");
        if (qFuzzyCompare(m_OutdoorTemperature, OutdoorTemperature))
            return;

        m_OutdoorTemperature = OutdoorTemperature;
        emit OutdoorTemperatureChanged(m_OutdoorTemperature);
        emit StateUpdate();
    }

    void setHumidity(float Humidity)
    {
        qWarning("Floating point comparison needs context sanity check");
        if (qFuzzyCompare(m_Humidity, Humidity))
            return;

        m_Humidity = Humidity;
        emit HumidityChanged(m_Humidity);
        emit StateUpdate();
    }

    void setPresure(float Presure)
    {
        qWarning("Floating point comparison needs context sanity check");
        if (qFuzzyCompare(m_Presure, Presure))
            return;

        m_Presure = Presure;
        emit PresureChanged(m_Presure);
        emit StateUpdate();
    }

signals:

    void FrontTemperatureChanged(float FrontTemperature);
    void RearTemperatureChanged(float RearTemperature);
    void OutdoorTemperatureChanged(float OutdoorTemperature);
    void HumidityChanged(float Humidity);
    void PresureChanged(float Presure);
    void StateUpdate();
};

