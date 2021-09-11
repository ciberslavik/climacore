#include "VentProfileEditorFrame.h"
#include "ui_VentProfileEditorFrame.h"

#include <Services/FrameManager.h>

#include <Frames/Dialogs/inputtextdialog.h>

VentProfileEditorFrame::VentProfileEditorFrame(MinMaxByDayProfile *profile, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentEditorFrame)
{
    ui->setupUi(this);
    setTitle("Редактирование профиля температуры");
    m_profile = profile;

    ui->txtName->setText(m_profile->Info.Name);
    ui->txtDescription->setText((m_profile->Info.Description));

    ui->lblCreationDate->setText(m_profile->Info.CreationTime.toString("dd.MM.yyyy hh:mm"));
    ui->lblEditDate->setText(m_profile->Info.ModifiedTime.toString("dd.MM.yyyy hh:mm"));

    m_model = new VentProfileModel(m_profile);

    ui->table->setModel(m_model);
    ui->table->resizeColumnsToContents();
    m_selection = ui->table->selectionModel();

    ui->table->selectColumn(0);
    drawGraph();

    connect(ui->txtName, &QClickableLineEdit::clicked, this, &VentProfileEditorFrame::onTxtClicked);
    connect(ui->txtDescription, &QClickableLineEdit::clicked, this, &VentProfileEditorFrame::onTxtClicked);
}

VentProfileEditorFrame::~VentProfileEditorFrame()
{
    disconnect(ui->txtName, &QClickableLineEdit::clicked, this, &VentProfileEditorFrame::onTxtClicked);
    disconnect(ui->txtDescription, &QClickableLineEdit::clicked, this, &VentProfileEditorFrame::onTxtClicked);
    delete ui;
}

QString VentProfileEditorFrame::getFrameName()
{
    return "VentProfileEditorFrame";
}


void VentProfileEditorFrame::on_btnAccept_clicked()
{
    m_profile->Info.Name = ui->txtName->text();
    m_profile->Info.Description = ui->txtDescription->text();

    emit editComplete();
    FrameManager::instance()->PreviousFrame();
}


void VentProfileEditorFrame::on_btnCancel_clicked()
{
    emit editCanceled();
    FrameManager::instance()->PreviousFrame();
}


void VentProfileEditorFrame::on_btnAddPoint_clicked()
{
    MinMaxByDayPoint point;
    MinMaxByDayEditDialog *dialog = new MinMaxByDayEditDialog(FrameManager::instance()->MainWindow());

    dialog->setValue(&point);
    dialog->setTitle("Добавление точки");
    dialog->setMaxValueName("Макс. вент.");
    dialog->setMinValueName("Мин. вент.");

    if(dialog->exec() == QDialog::Accepted)
    {
        ui->table->setModel(nullptr);
        m_profile->Points.append(point);

        qSort(m_profile->Points.begin(), m_profile->Points.end(),
              [](const MinMaxByDayPoint &a, const MinMaxByDayPoint &b) -> bool { return a.Day < b.Day; });

        ui->table->setModel(m_model);
        ui->table->resizeColumnsToContents();
        drawGraph();
    }

}

void VentProfileEditorFrame::onTxtClicked()
{
    QLineEdit *txt = dynamic_cast<QLineEdit*>(sender());
    InputTextDialog *dlg = new InputTextDialog(FrameManager::instance()->MainWindow());

    dlg->setText(txt->text());

    if(dlg->exec() == QDialog::Accepted)
    {
        txt->setText(dlg->getText());
    }

}


void VentProfileEditorFrame::on_btnEditPoint_clicked()
{
    m_selection = ui->table->selectionModel();
    int currentIndex = 0;
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        currentIndex = indexes.at(0).column();
        MinMaxByDayPoint point = m_profile->Points.at(currentIndex);

        MinMaxByDayEditDialog *dialog = new MinMaxByDayEditDialog(FrameManager::instance()->MainWindow());

        dialog->setValue(&point);
        dialog->setTitle("Редактирование точки");
        dialog->setMaxValueName("Макс. вент.");
        dialog->setMinValueName("Мин. вент.");

        if(dialog->exec() == QDialog::Accepted)
        {
            ui->table->setModel(nullptr);
            m_profile->Points[currentIndex] = point;

            qSort(m_profile->Points.begin(), m_profile->Points.end(),
                  [](const MinMaxByDayPoint &a, const MinMaxByDayPoint &b) -> bool { return a.Day < b.Day; });


            ui->table->setModel(m_model);
            ui->table->resizeColumnsToContents();
            drawGraph();
        }
    }

}


void VentProfileEditorFrame::on_btnRemovePoint_clicked()
{
    m_selection = ui->table->selectionModel();
    int currentIndex = 0;
    QModelIndexList indexes = m_selection->selectedIndexes();
    if(indexes.count() > 0)
    {
        currentIndex = indexes.at(0).column();
        m_model->removePoint(currentIndex);

        qSort(m_profile->Points.begin(), m_profile->Points.end(),
              [](const MinMaxByDayPoint &a, const MinMaxByDayPoint &b) -> bool { return a.Day < b.Day; });


        ui->table->setModel(m_model);
        ui->table->resizeColumnsToContents();
        drawGraph();
    }
}


void VentProfileEditorFrame::on_btnLeft_clicked()
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


void VentProfileEditorFrame::on_btnRight_clicked()
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

void VentProfileEditorFrame::selectColumn(int column)
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

void VentProfileEditorFrame::drawGraph()
{
    QCustomPlot *plot = ui->plot;
    plot->clearGraphs();


    qDebug()<< "graphs count"<< plot->graphCount();
    int pointCount = m_profile->Points.count();

    QVector<double> days(pointCount);
    QVector<double> maxValue(pointCount);
    QVector<double> minValue(pointCount);

    for(int i = 0; i < pointCount; i++)
    {
        days[i] = m_profile->Points[i].Day;
        maxValue[i] = m_profile->Points[i].MaxValue;
        minValue[i] = m_profile->Points[i].MinValue;
    }

    plot->addGraph();
    plot->graph()->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssCircle, 5));
    plot->graph()->setData(days, maxValue);
    plot->graph()->rescaleAxes();

    plot->addGraph();
    plot->graph()->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssCircle, 5));
    plot->graph()->setData(days, minValue);
    plot->graph()->rescaleAxes();

    plot->xAxis->setRange(1,50);
    plot->yAxis->setRange(0,100);

    plot->replot();
}

