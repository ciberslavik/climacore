#pragma once

#include <QItemSelectionModel>
#include <QWidget>

#include <Frames/FrameBase.h>
#include <Models/Graphs/MinMaxByDayProfile.h>
#include <Models/Dialogs/VentProfileModel.h>
#include <Frames/Dialogs/MinMaxByDayEditDialog.h>
namespace Ui {
class VentEditorFrame;
}

class VentProfileEditorFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentProfileEditorFrame(MinMaxByDayProfile *profile, QWidget *parent = nullptr);
    ~VentProfileEditorFrame();
    QString getFrameName() override;

signals:
    void editComplete();
    void editCanceled();
private slots:

    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

    void on_btnAddPoint_clicked();
    void onTxtClicked();
    void on_btnEditPoint_clicked();

    void on_btnRemovePoint_clicked();

    void on_btnLeft_clicked();

    void on_btnRight_clicked();

private:
    Ui::VentEditorFrame *ui;
    MinMaxByDayProfile *m_profile;
    VentProfileModel *m_model;
    QItemSelectionModel *m_selection;

    void selectColumn(int column);

    void drawGraph();
};

