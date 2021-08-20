#include "QClickableLabel.h"

#include <QEvent>

QClickableLabel::QClickableLabel(QWidget *parent) : QLabel(parent)
{

}

bool QClickableLabel::event(QEvent *myEvent)
{
    if(myEvent->type() == QEvent :: MouseButtonRelease)   // Identify Mouse press Event
    {
        emit labelClicked();
    }

    return QWidget::event(myEvent);
}
