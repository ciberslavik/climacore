/********************************************************************************
** Form generated from reading UI file 'MainWindow.ui'
**
** Created by: Qt User Interface Compiler version 5.9.9
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QFrame>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    QGridLayout *gridLayout;
    QLabel *label_6;
    QLabel *lblHeads;
    QLabel *lblDay;
    QLabel *label;
    QLabel *lblStatus_2;
    QLabel *label_3;
    QLabel *lblStatus;
    QLabel *lblClock;
    QFrame *mainFrame;
    QLabel *lblTitle;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QStringLiteral("MainWindow"));
        MainWindow->resize(800, 480);
        MainWindow->setMinimumSize(QSize(800, 480));
        MainWindow->setMaximumSize(QSize(800, 480));
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName(QStringLiteral("centralwidget"));
        gridLayout = new QGridLayout(centralwidget);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        label_6 = new QLabel(centralwidget);
        label_6->setObjectName(QStringLiteral("label_6"));
        label_6->setMaximumSize(QSize(110, 16777215));
        QFont font;
        font.setPointSize(14);
        label_6->setFont(font);

        gridLayout->addWidget(label_6, 0, 4, 1, 1);

        lblHeads = new QLabel(centralwidget);
        lblHeads->setObjectName(QStringLiteral("lblHeads"));
        lblHeads->setFont(font);

        gridLayout->addWidget(lblHeads, 0, 5, 1, 1);

        lblDay = new QLabel(centralwidget);
        lblDay->setObjectName(QStringLiteral("lblDay"));
        lblDay->setMaximumSize(QSize(50, 16777215));
        lblDay->setFont(font);

        gridLayout->addWidget(lblDay, 0, 3, 1, 1);

        label = new QLabel(centralwidget);
        label->setObjectName(QStringLiteral("label"));
        label->setMaximumSize(QSize(100, 20));
        label->setFont(font);

        gridLayout->addWidget(label, 0, 0, 1, 1);

        lblStatus_2 = new QLabel(centralwidget);
        lblStatus_2->setObjectName(QStringLiteral("lblStatus_2"));
        lblStatus_2->setMaximumSize(QSize(140, 20));
        lblStatus_2->setFont(font);

        gridLayout->addWidget(lblStatus_2, 0, 1, 1, 1);

        label_3 = new QLabel(centralwidget);
        label_3->setObjectName(QStringLiteral("label_3"));
        label_3->setMaximumSize(QSize(50, 16777215));
        label_3->setFont(font);

        gridLayout->addWidget(label_3, 0, 2, 1, 1);

        lblStatus = new QLabel(centralwidget);
        lblStatus->setObjectName(QStringLiteral("lblStatus"));
        lblStatus->setMinimumSize(QSize(0, 15));
        lblStatus->setMaximumSize(QSize(16777215, 17));
        QFont font1;
        font1.setPointSize(12);
        lblStatus->setFont(font1);

        gridLayout->addWidget(lblStatus, 3, 5, 1, 1);

        lblClock = new QLabel(centralwidget);
        lblClock->setObjectName(QStringLiteral("lblClock"));
        lblClock->setAlignment(Qt::AlignRight|Qt::AlignTrailing|Qt::AlignVCenter);

        gridLayout->addWidget(lblClock, 0, 6, 1, 1);

        mainFrame = new QFrame(centralwidget);
        mainFrame->setObjectName(QStringLiteral("mainFrame"));
        mainFrame->setFrameShape(QFrame::StyledPanel);
        mainFrame->setFrameShadow(QFrame::Raised);

        gridLayout->addWidget(mainFrame, 2, 0, 1, 7);

        lblTitle = new QLabel(centralwidget);
        lblTitle->setObjectName(QStringLiteral("lblTitle"));
        lblTitle->setMinimumSize(QSize(0, 20));
        lblTitle->setMaximumSize(QSize(16777215, 20));
        lblTitle->setFont(font);
        lblTitle->setAlignment(Qt::AlignCenter);

        gridLayout->addWidget(lblTitle, 1, 0, 1, 7);

        MainWindow->setCentralWidget(centralwidget);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QApplication::translate("MainWindow", "MainWindow", Q_NULLPTR));
        label_6->setText(QApplication::translate("MainWindow", "\320\237\320\276\320\263\320\276\320\273\320\276\320\262\321\214\320\265", Q_NULLPTR));
        lblHeads->setText(QApplication::translate("MainWindow", "20000", Q_NULLPTR));
        lblDay->setText(QApplication::translate("MainWindow", "45", Q_NULLPTR));
        label->setText(QApplication::translate("MainWindow", "\320\241\320\276\321\201\321\202\320\276\321\217\320\275\320\270\320\265:", Q_NULLPTR));
        lblStatus_2->setText(QApplication::translate("MainWindow", "\320\222\321\213\321\200\320\260\321\211\320\270\320\262\320\260\320\275\320\270\320\265", Q_NULLPTR));
        label_3->setText(QApplication::translate("MainWindow", "\320\224\320\265\320\275\321\214", Q_NULLPTR));
        lblStatus->setText(QApplication::translate("MainWindow", "TextLabel", Q_NULLPTR));
        lblClock->setText(QApplication::translate("MainWindow", "TextLabel", Q_NULLPTR));
        lblTitle->setText(QApplication::translate("MainWindow", "TextLabel", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
