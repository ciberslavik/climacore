#pragma once

#include <QWidget>

class FrameBase : public QWidget
{
    Q_OBJECT

    Q_PROPERTY(QString Title READ Title WRITE setTitle NOTIFY TitleChanged)
    QString m_Title;

public:
    explicit FrameBase(QWidget *parent = nullptr);
    virtual QString getFrameName() = 0;


QString Title() const
{
    return m_Title;
}

public slots:
void setTitle(QString Title)
{
    if (m_Title == Title)
        return;

    m_Title = Title;
    emit TitleChanged(m_Title);
}

signals:

void TitleChanged(QString Title);
};

