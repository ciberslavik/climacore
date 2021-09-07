#pragma once

#include <QItemSelectionModel>
#include <QWidget>

#include <Frames/FrameBase.h>
#include <Models/Graphs/ValueByDayProfile.h>
#include <Models/Dialogs/TempProfileModel.h>
#include <Frames/Dialogs/ValueByDayEditDialog.h>
namespace Ui {
class GraphEditorFrame;
}

class TempProfileEditorFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit TempProfileEditorFrame(const ValueByDayProfile &profile, QWidget *parent = nullptr);
    ~TempProfileEditorFrame();
    QString getFrameName() override;

signals:
    void editComplete(const ValueByDayProfile &profile);
    void editCanceled();
private slots:
    void tableSelectionChanged();
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

    void on_btnAddPoint_clicked();
    void onTxtClicked();
private:
    Ui::GraphEditorFrame *ui;
    ValueByDayProfile m_profile;
    TempProfileModel *m_model;
    QItemSelectionModel *m_selection;


};

