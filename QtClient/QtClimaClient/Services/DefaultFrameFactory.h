#pragma once

#include <Frames/IFrameFactory.h>
#include <QObject>

#include <Frames/IFrameFactory.h>

class DefaultFrameFactory : public QObject, public IFrameFactory
{
    Q_OBJECT
public:
    DefaultFrameFactory();

    // IFrameFactory interface
public:
    virtual FrameBase *CreateFrame(const QString &frameName) override;
};

