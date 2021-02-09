using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Charts;
using Gestion_Etudiant__WPF.Services;
using pBrushes = System.Windows.Media.Brushes;

namespace Gestion_Etudiant__WPF.GestionStatistiques
{
    /// <summary>
    /// Logique d'interaction pour Statistiques.xaml
    /// </summary>
    public partial class Statistiques : UserControl
    {
        private ServiceFiliere serviceFiliere = new ServiceFiliere();
        private ServiceEtudiant serviceEtudiant = new ServiceEtudiant();
        private Dictionary<String, int> nbEtudiantFiliere = new Dictionary<String, int>();
        public Statistiques()
        {
            InitializeComponent();

            getData();
            draw();
        }

        private void getData()
        {
            DataTable dataFiliere = serviceFiliere.FillData();
            DataTable dataEtudiant = serviceEtudiant.FillData();

            for (int i = 0; i < dataFiliere.Rows.Count; i++)
            {
                nbEtudiantFiliere.Add(dataFiliere.Rows[i]["FiliereName"].ToString(), 0);
            }

            for (int i = 0; i < dataEtudiant.Rows.Count; i++)
            {
                int idFiliere = Convert.ToInt32(dataEtudiant.Rows[i]["ID_Filiere"]);
                String nomFiliere = dataFiliere.Rows[idFiliere - 1]["FiliereName"].ToString();
                nbEtudiantFiliere[nomFiliere] += 1;
            }
        }

        private void draw()
        {
            SolidColorBrush[] brushesArray = new SolidColorBrush[]
            {
                pBrushes.Blue,
                pBrushes.Red,
                pBrushes.Lime,
                pBrushes.Purple,
                pBrushes.SkyBlue,
                pBrushes.Orange
            };

            int k = 0;
            foreach (var  kvp in nbEtudiantFiliere)
            {
                BarSeries3D bar = new BarSeries3D();
                SeriesPoint seriesPoint = new SeriesPoint();
                CustomLegendItem legendItem = new CustomLegendItem();

                seriesPoint.Argument = kvp.Key;
                seriesPoint.Value = kvp.Value;

                bar.Points.Add(seriesPoint);
                bar.LabelsVisibility = true;

                legendItem.Text = kvp.Key;
                legendItem.MarkerBrush = brushesArray[k];
                k++;


                this.Diagram3D.Series.Add(bar);
                this.legend1.CustomItems.Add(legendItem);
            }
        }
    }
}
