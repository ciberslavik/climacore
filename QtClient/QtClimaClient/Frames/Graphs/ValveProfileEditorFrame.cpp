#include "ValveProfileEditorFrame.h"
#include "ui_ValveProfileEditorFrame.h"

#include <Services/FrameManager.h>

#include <Frames/Dialogs/inputtextdialog.h>

ValveProfileEditorFrame::ValveProfileEditorFrame(ValueByValueProfile *profile, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::GraphEditorFrame)
{
    ui->setupUi(this);
    setTitle("Редактирование профиля температуры");
    m_profile = profile;

    ui->txtName->setText(m_profile->Info.Name);
    ui->txtDescription->setText((m_profile->Info.Description));

    ui->lblCreationDate->setText(m_profile->Info.CreationTime.toString("dd.MM.yyyy hh:mm"));
    ui->lblEditDate->setText(m_profile->Info.ModifiedTime.toString("dd.MM.yyyy hh:mm"));

    m_model = new TempProfileModel(m_profile);

    ui->table->setModel(m_model);
    ui->table->resizeColumnsToContents();
    m_selection = ui->table->selectionModel();

    ui->table->selectColumn(0);
    drawGraph();

    connect(ui->txtName, &QClickableLineEdit::clicked, this, &ValveProfileEditorFrame::onTxtClicked);
    connect(ui->txtDescription, &QClickableLineEdit::clicked, this, &ValveProfileEditorFrame::onTxtClicked);
}

ValveProfileEditorFrame::~ValveProfileEditorFrame()
{
    disconnect(ui->txtName, &QClickableLineEdit::clicked, this, &ValveProfileEditorFrame::onTxtClicked);
    disconnect(ui->txtDescription, &QClickableLineEdit::clicked, this, &ValveProfileEditorFrame::onTxtClicked);
    delete ui;
}

QString ValveProfileEditorFrame::getFrameName()
{
    return "ValveProfileEditorFrame";
}


void ValveProfileEditorFrame::on_btnAccept_clicked()
{
    m_profile->Info.Name = ui->txtName->text();
    m_profile->Info.Description = ui->txtDescription->text();

    emit editComplete();
    FrameManager::instance()->PreviousFrame();
}


void ValveProfileEditorFrame::on_btnCancel_clicked()
{
    emit editCanceled();
    FrameManager::instance()->PreviousFrame();
}


void ValveProfileEditorFrame::on_btnAddPoint_clicked()
{
    ValueByValuePoint point;
    ValueByValueEditDialog *dialog = new ValueByValueEditDialog(FrameManager::instance()->MainWindow());

    dialog->setValue(&point);
    dialog->setTitle("Добавление точки");
    dialog->setValueName("Температура");

    if(dialog->exec() == QDialog::Accepted)
    {
        ui->table->setModel(nullptr);
        m_profile->Points.append(point);

        qSort(m_profile->Points.begin(), m_profile->Points.end(),
              [](const ValueByValuePoint &a, const ValueByValuePoint &b) -> bool { return a.ValueX < b.ValueX; });

        ui->table->setModel(m_model);
        ui->table->resizeColumnsToContents();
        drawGraph();
    }

}

void ValveProfileEditorFrame::onTxtClicked()
{
    QLineEdit *txt = dynamic_cast<QLineEdit*>(sender());
    InputTextDialog *dlg = new InputTextDialog(FrameManager::instance()->MainWindow());

    dlg->setText(txt->text());

    if(dlg->exec() == QDialog::Accepted)
    {
        txt->setText(dlg->getText());
    }

}


void ValveProfileEditorFrame::on_btnEditPoint_clicked()
{
    m_selection = ui->table->selectionModel();
    int currentIndex = 0;
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        currentIndex = indexes.at(0).column();
        ValueByDayPoint point = m_profile->Points.at(currentIndex);

        ValueByDayEditDialog *dialog = new ValueByDayEditDialog(FrameManager::instance()->MainWindow());

        dialog->setValue(&point);
        dialog->setTitle("Редактирование точки");
        dialog->setValueName("Температура");

        if(dialog->exec() == QDialog::Accepted)
        {
            ui->table->setModel(nullptr);
            m_profile->Points[currentIndex] = point;

            qSort(m_profile->Points.begin(), m_profile->Points.end(),
                  [](const ValueByValuePoint &a, const ValueByValuePoint &b) -> bool { return a.ValueX < b.ValueX; });


            ui->table->setModel(m_model);
            ui->table->resizeColumnsToContents();
            drawGraph();
        }
    }

}


void ValveProfileEditorFrame::on_btnRemovePoint_clicked()
{
    m_selection = ui->table->selectionModel();
    int currentIndex = 0;
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        currentIndex = indexes.at(0).column();
        m_profile->Points.removeAt(currentIndex);

        qSort(m_profile->Points.begin(), m_profile->Points.end(),
              [](const ValueByValuePoint &a, const ValueByValuePoint &b) -> bool { return a.ValueX < b.ValueX; });


        ui->table->setModel(m_model);
        ui->table->resizeColumnsToContents();
        drawGraph();
    }
}


void ValveProfileEditorFrame::on_btnLeft_clicked()
{
    m_selection = ui->table->selectionModel();
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count()>0)
    {
        int currentIndex = indexes.at(0).column();
        if(currentIndex == 0)
            selectColumn(currentIndex);
        else
            selectColumn(currentIndex - 1);
    }
    else
        selectColumn(0);
}


void ValveProfileEditorFrame::on_btnRight_clicked()
{
    m_selection = ui->table->selectionModel();
    int currentIndex = 0;
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        currentIndex = indexes.at(0).column();

        if(currentIndex == (m_model->columnCount()-1))
            ui->table->selectColumn(currentIndex);
        else
            ui->table->selectColumn(currentIndex + 1);
    }
    else
        selectColumn(0);
}

void ValveProfileEditorFrame::selectColumn(int column)
{
    QModelIndex left;
    QModelIndex right;

    left = m_model->index(0, column, QModelIndex());
    right = m_model->index(m_model->rowCount() - 1, column, QModelIndex());

    QItemSelection selection(left, right);
    m_selection = ui->table->selectionModel();
    m_selection->clear();
    m_selection->select(selection, QItemSelectionModel::Select);

    ui->table->setSelectionModel(m_selection);
}

void ValveProfileEditorFrame::drawGraph()
{
    QCustomPlot *plot = ui->plot;

    plot->addGraph();
    int pointCount = m_profile->Points.count();

    QVector<double> days(pointCount);
    QVector<double> temps(pointCount);

    for(int i = 0; i < pointCount; i++)
    {
        days[i] = m_profile->Points[i].Day;
        temps[i] = m_profile->Points[i].Value;
    }
    plot->graph(0)->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssCircle, 5));
    plot->graph(0)->setData(days, temps);

    plot->xAxis->setRange(0,100);
    plot->yAxis->setRange(0,100);

    plot->replot();
}

