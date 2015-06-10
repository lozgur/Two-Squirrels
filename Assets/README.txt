

//animasyonlar animasyon klasörünün içinde
//kullandığım assetler images klasörünün içinde
//scriptler scripts klasörünün içinde
//sesler sound klasörünün içinde
// sahneler scene klasörünün içinde
// oyuna başlamak için scene klasörü içinde main menu sahnesini seçip play butonuna basınız.

Oyunun yapılışı

// Önce projemin konusunu belirledim.

// Konusunu belirledikten sonra projem için gerekli assetleri internetten buldum.

// Assetleri photoshop programı kullanarak projem için hazır hale getirdim.

// Unity5.1 (2D)projemin içine onları drag edip üzerinde değişiklik yaptığımda yada tekrar kullanmak istediğimde rahatlık olsun diye;
   bana gerekli olacak assetleri prefab haline getirdim.

// Projem için gerekli animasyonları hazırladım(bulduğum resimler animasyon için yeterliydi).
   Animasyonlar için gerekli resimleri şöyle hazırladım;
   -sprite mode kısmından multiple seçeneğini işaretledim.
   -Sprite Editor kullanarak resimleri kestim
   -Resimlerin oynamaması için kesme kısmındaki noktayı bir yere sabitledim.
   Animasyon resimlerini hazırladıktan sonra animasyonumu oluşturup animation klasöründe topladım hepsini.
   Animator penceresini kullanarak state lerini hazırladım. Bazı animasyonlarda (asansör kolu gibi) loop seçmesini kapadım ve
   animasyonun çalışıp çalışmadığı durumunu tutucak bir bool cinsinden parametre tanımladım.

// Level larımı  hazırladım . Kullandığım asset'lere birbirlerini tanıyabilmeleri için Polygon collider2d ekledim.
   Kolaylık olsun diye , karakterimin çarptığı zaman tanıması gereken objelere, isimleriyle teker teker bulup kontrol etmek zor olucak diye
   tag oluşturdum(ateş , su , düğme , asansör kolu). Canvas kullanarak kırmızı ve mavi sincabın puanlarını tutacak iki tane image yarattım 
   ve içlerine puanları gösterebilmeleri için text koydum. aynı şekilde zamanı göstermek içinde bi tane image yaratıp içine çocuğu olarak text yarattım

// Level'lar hazır olduktan sonra kodlarımı yazmaya başladım.

// İlk önce oyunu yönetmesi için game manager(GM) kodu hazırladım;

// GM kodu içinde;
     -parametreler;
	- int KırmızıPuan : kırmızı sincabın topladığı puanı tutmak için.
	- int MaviPuan : Mavi sincabın topladığı puanı tutmak için.
	- bool PauseGame : oyunun duraklatılıp duraklatılmadığı durumunu tutmak için.
	- bool EnterBlue : mavi sincabın mavi kapıya girip girmediği durumunu tutması için.
	- bool EnterRed : kırmızı sincabın kırmıza kapıya girip girmediği durumunu tutması için. 
	- int GameTime : her level 60 saniyede tamamlanmak zorunda bu saniyeyi tutması için.
     -fonksiyonlar;
	-void ContinueButton ()-> Ara menü sayfasında continue butonuna basıldığında çağırılan fonksiyon
				  Zaman akmaya başlar, Pausegame=false olur , 
	  			  Ekrana çıkan ara menü ve butonları kapatır.
	-void SetRedTime ()-> kırmızı sincap level'i başarıyla tamamlandığı zaman puanına kalan süre eklenir. 
	-void SetBlueTime ()-> mavi sincap level'i başarıyla tamamlandığı zaman puanına kalan süre eklenir. 
	-void AddBluePoint () -> mavi sincap mavi elmasları topladığı zaman her topladığı elmas için puanına 10 puan eklenir. 
	-void AddKırmızıPoint () -> kırmızı sincap kırmızı elmasları topladığı zaman her topladığı elmas için puanına 10 puan eklenir.
	-void Update () -> 
			   * Gametime 0 ise GameTime'ı oyunun zamanına eşitledim çünkü level'de başarısız olduğun zaman tekrar oynadığında 
			     zaman bi önceki kaldığı yerden devam ediyordu.
			   * Geçen süre 60 olduğu zaman , GameTime'ı 0 'a eşitleyip (tekrar yüklendiğinde zaman kaldığı yerden devam etmesin diye)
			     Gameover sahnesini çalıştırdım.  
			   * EnterRed ve EnterBlue true olduğu zaman yani iki sincapta kapıdan içeri girdiği zaman bir sonraki level'ı çalıştırdım.
			   * Escape tuşuna basıldığında oyun duraksamada değilse , zamanı duraksattım pausegame=true yaptım ve ara menü ve butonlarını görünür yaptım.
			   * Escape tuşuna basıldığında oyun duraksamadaysa , zamanı başlattım ve  pausegame=false yaptım ve ara menü ve butonların görünürlüğünü kaldırdım.
			   * zaman ve sincapların puanlarını göstermesi için yarattığım image'ların içindeki text'leri buldurup içine gösterecekleri değerleri atadım.

// Controller kodu içinde;
     -parametreler;
	-bool boundry : zıplama süresi.
	- bool jumped :  zıplayıp zıplamadığını tutmak için.
	- GM gm : controller script'inden GM script'ine ulaşmak için.
	- Color x : sincaplar ateşe veya suya değdiğinde çıkan bulut animasyonunun ekrandan yavaş yavaş silinmesi için.
	- Animator anim : Animator'a ulaşmak için.
	- bool dead : sincapların ölüp ölmediğini tutması için. 
	- float speed : sincapların hızlarını tutmak için.

     -fonksiyonlar ;
	-void OnCollisionStay2D()-> sincap taşa çıktığı zaman sincabı taşın çocuğu  yapıyo ki taşla beraber yukarı çıksın.	
	-void OnCollisionExit2D()-> sincap taştan ayrıldığı zaman, sincabın artık taşın çocuğu olmamasını sağlıyor.
   	-void OnTriggerEnter2D() -> *eğer sincap anahtara değerse anahtar yok oluyo kapıyı tanıması için 
				     kapının collider ı çalışır hale getiriliyor ve kapının animasyonu çalıştırılıyor.
				    *sincaplar elmaslara değdiği zaman elmas yok oluyor ve GM script'indeki addRedPoint() fonksiyonu çağırılıp puan ekleniyor
				    *sincap eğer kapıdan geçerse yok oluyor ve GM kodundaki setRedTime fonksiyonu çağırılıp son puanı hesaplanıyor.
				    *sincap ateşe veya suya değerse sincabın sprite ı kaldırılıp yerine patlama bulutunun sprite ı koyulup patlama animasyonu başlatılıyor.
				     sonra dead=true yapılıyor ve SpriteAlfaKapa() coroutine fonksiyonu çağırılıyor.
	- IEnumarator SpriteAlfaKapa()-> ekranda çıkan patlama bulutunun renginin alfasını yavaş yavaş kapatarak ekranda yavaş yavaş kaybolmasını sağlıyor ve gameover sahnesini açıyor.
	- void Start() -> parametrelerin ilk değerleri atanıyor.
	- void Movement() -> eğer klavyede sağtuşa basılıyorsa sağa gitme , sol tuşa basılıyorsa sola gitmesini sağlıyor ve bunların animasyonlarını çalıştırıyor.
			     yukarı tuşuna basılırsa zıplamayı sağlıyor(zıplama esnasında bir daha zıplamasın diye jumped ile zıplayıp zıplamadığı kontrol ediliyor)
 
// Asansor kodu içinde(bu kod asansörü çalıştıracak objelerin(düğmeler ve asansör kolları ) içindedir);
      -parametreler;
	-Vector3 position1 , position2 : taşın nerden nereye gideceğini tutar.
	-Gameobject Asansor : Hangi taşın hareket edeceğini söylemek ve hareket ettirmek için.
      -fonksiyonlar;
	-void awake() -> parametrelerin ilk değerleri atanıyor.
	-void OntriggerStay() -> sincaplarla temas halinde olduğu sürece;
				 script hangi objenin içindeyse onun animasyonu çalışıyor ve sincapların geçebilmeleri için taş hareket ettiriliyor.
	-void OnTriggerExit() -> sincaplarla temas bittiğinde animasyon durdurulup taş yerine geri döndürülüyor.

// UI kodu içinde(UI kodu level'ların çalışma düzenini düzenlemek için yazılmıştır.) ;
       -parametreler: 
	-Gm gm : bu scripten gm'e ulaşabilmek için.
       -fonksiyonlar:
	-void start() -> eğer şuanda main menüde yada gameover da değilsek gm in değeri atanıyor.
	-void PlayButton()-> play button ' a basıldığında bu fonksiyon çağırılıp bir sonraki level yükleniyor.
	-void ContinueButton()-> Continue button'a basıldığında gm'deki ContinueButton() fonksiyonu çağırılıyor.
	-void QuitMainMenuButton()-> Main menüye dönmeyi sağlıyor.
	-void QuitButton() -> oyundan çıkmayı sağlıyor.
