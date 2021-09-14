#pragma once

#include <QItemSelectionModel>
#include <QWidget>

#include <Frames/FrameBase.h>
#include <Models/Graphs/ValueByValueProfile.h>
#include <Models/Dialogs/ValveProfileModel.h>
#include <Frames/Dialogs/ValueByValueEditDialog.h>
namespace Ui {
class GraphEditorFrame;
}

class ValveProfileEditorFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit ValveProfileEditorFrame(ValueByValueProfile *profile, QWidget *parent = nullptr);
    ~ValveProfileEditorFrame();
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
    Ui::GraphEditorFrame *ui;
    ValueByValueProfile *m_profile;
    ValveProfileModel *m_model;
    QItemSelectionModel *m_selection;

    void selectColumn(int column);

    void drawGraph();
};

