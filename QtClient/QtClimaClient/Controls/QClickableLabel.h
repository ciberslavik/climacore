#ifndef QCLICKABLELABEL_H
#define QCLICKABLELABEL_H

#include <QWidget>
#include <QLabel>

class QClickableLabel : public QLabel
{
    Q_OBJECT
public:
    explicit QClickableLabel(QWidget *parent = nullptr);

signals:
    void labelClicked();       // Signal to emit

protected:
    bool event(QEvent *myEvent); // This method will give all kind of events on Label Widget

};



#endif // QCLICKABLELABEL_H
