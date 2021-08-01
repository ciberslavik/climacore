/********************************************************************************
** Form generated from reading UI file 'VentelationStateFrame.ui'
**
** Created by: Qt User Interface Compiler version 5.9.9
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_VENTELATIONSTATEFRAME_H
#define UI_VENTELATIONSTATEFRAME_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_VentelationStateFrame
{
public:

    void setupUi(QWidget *VentelationStateFrame)
    {
        if (VentelationStateFrame->objectName().isEmpty())
            VentelationStateFrame->setObjectName(QStringLiteral("VentelationStateFrame"));
        VentelationStateFrame->resize(400, 300);

        retranslateUi(VentelationStateFrame);

        QMetaObject::connectSlotsByName(VentelationStateFrame);
    } // setupUi

    void retranslateUi(QWidget *VentelationStateFrame)
    {
        VentelationStateFrame->setWindowTitle(QApplication::translate("VentelationStateFrame", "Form", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class VentelationStateFrame: public Ui_VentelationStateFrame {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_VENTELATIONSTATEFRAME_H
