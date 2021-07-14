using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.Reflection;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
   
    public partial class MainWindow : Window
    {
       
        // oyun tamamlama süresi için bir değişken tanımladım
        int saniye = 0;

        // sorularda ilerleme yapabilmek ve dizilerden elemanları çekmek için bir değişken tanımladım
        int sayaç = 0;

        // oyuncuya puanını gösterebilmek için bir puan değişkeni tanımaladım
        int puan = 0;
     
        public MainWindow()
        {
            InitializeComponent();
          
        }

        /// <summary>
        /// <seealso cref="DispatcherTimer"/> zamanlama tutmak için Dispatcher sınıfını çektim
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();

        // Verileri tutmak için diziler tanımladım. bir veritabanından veriler çekilerek de kullanılabilir
        string [] resim = { "A.jpg", "b.jpg", "c.jpeg","d.jpeg","e.jpeg","kupa.jpg"};
        string[] sorular = { "Ekranda Görülen Lider Kimdir?", "Ekranda Görülen yemek hangi ilimizin meşhur yemeğidir?", "'Kaplumbağa Terbiyecisi' Adlı Tablonun Sahibi Kimdir?","Bu Meyve hangi mevsim ile özdeşleşmiştir ?", "Shrek filmi kaç film serisinden oluşmaktadır", "GAME OVER!!"};
        string[,] cevap = { { "V. George", "İsmet inönü", "Atatürk", "Adolf Hitler" } , { "Ankara", "Bursa", " Hatay", "Balıkesir" } , { "Osman Hamdi Bey", "İbrahim Çallı", " Hoca Ali Rıza", "Abidin Dino" } , { "Sonbahar", "Kış", " İlkbahar", "Yaz" }, { "5", "4", " 3", "2" }, { "", "", " ", "" } };
        string[] dogrucevap = {"Atatürk" , "Ankara", "Osman Hamdi Bey", "Yaz","5",""};

        /// <summary>
        /// timer_Tick eventi ile saniyeyi ilerleterek label ile ekrana yazdırdım
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_Tick(object sender, EventArgs e)

        {
            saniye++;
            timerlabel.Content = saniye.ToString();
            
            
        }

     

       /// <summary>
       /// baslat_Click eventim ile başlat butonu ile oyunumu başlattım kullanıcının önüne resimi ve soruları gösterdim.
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void baslat_Click(object sender, RoutedEventArgs e)

        {
            
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            try
            {
                // zamanlayıcıyı başlattım 
                timer.Start();

                // seçenekleri visible (gizleme) özelliğini çağırdım.
                secenekgizle();

                // verilerimi çektim
                yaz();
            }
            catch
            {
                // Başlatma esnasında karşılaşılabilecek hatalara karşı bir hata mesajı ekledim.
                MessageBox.Show("Başlatma Hatası");
            }
          

        }
        /// <summary>
        /// yaz() metodu ile ekrana resim çektim soruları ve şıkları yazdırdım.
        /// </summary>
        public void yaz()
        {
            // Resimleri çekerken proje içine resimleri ekleyerek assembly yoluyla çektim.
            string assembly_prefix = "pack://application:,,,/" + Assembly.GetEntryAssembly().GetName().Name + ";component/Resources/";
            image1.Source = new BitmapImage(new Uri(assembly_prefix + resim[sayaç]));

            // soruları diziden çekerek label'a yazdırdım.
            soru.Content = sorular[sayaç];

            //soru şıklarını radiobutton üzerine yazdım.
            a.Content = cevap [sayaç,0];
            b.Content = cevap[sayaç, 1];
            c.Content = cevap[sayaç, 2];
            d.Content = cevap[sayaç, 3];
           

        }
        /// <summary>
        /// başlangıç ekranında şıkları gizlememi sağlıyor görüntü kirliliğini engelliyor
        /// </summary>
        public void secenekgizle()
        {
            a.Visibility = Visibility.Visible;
            b.Visibility = Visibility.Visible;
            c.Visibility = Visibility.Visible;
            d.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Seçilen şıkkın doğru olup olmadığını kontrol ediyor Puan değişkenine puan ataması yapıyor. 
        /// </summary>
        public void Puanla()
        {
            if (a.IsChecked == true && a.Content == dogrucevap[sayaç])
            {
                puan = puan + 20;
            }
            else if (b.IsChecked == true && b.Content == dogrucevap[sayaç])
            {
                puan = puan + 20;
            }
            else if (c.IsChecked == true && c.Content == dogrucevap[sayaç])
            {
                puan = puan + 20;
            }
            else if (d.IsChecked == true && d.Content == dogrucevap[sayaç])
            {
                puan = puan + 20;
            }
            puantext.Content = puan.ToString();
        }
        /// <summary>
        /// seçilen şıkkın diğer soruya geçtiğinde işaretli kalmasını 
        /// engellemek uygulamanın daha verimli çalışması için soru sonrası seçenek seçmelerini temizliyoruz.
        /// </summary>
        public void secenektemizle()
        {
            a.IsChecked = false;
            b.IsChecked = false;
            c.IsChecked = false;
            d.IsChecked = false;
        }

       
  
        /// <summary>
        /// Diğer soruya geçmeyi sağlıyor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ileri_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // puanlama metodunu çağırıyor.
                Puanla();

                // bu sayaç ile dizilerde veri ilerleyişini sağlıyor.
                sayaç++;

                // secenektemizle() metodunu çağırıyor
                secenektemizle();

                // yaz() metodunu çağırıyor.
                yaz();

                // ileri butonunu bitti olarak değiştiriyor
                if (sayaç == 4)
                {
                    ileri.Content = "bitti";

                }

                // soruların sonuna geldiğinde puan ve süreyi ekranda göstererek programı sonlandırıyor
                else if (sayaç == 5)
                {
                    // sayacı sonlandırıyor
                    timer.Stop();

                    MessageBox.Show("Tebrikler Puanınız : " + puan.ToString() + "  Cevaplama Süreniz  " + timerlabel.Content) ;

                    // Programı sonlandırıyor
                    this.Close();

                }
            }
            catch
            {

            }

           
        }

        private void a_Checked(object sender, RoutedEventArgs e)
        {
         

        }
    }
}
