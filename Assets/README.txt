

//animasyonlar animasyon klas�r�n�n i�inde
//kulland���m assetler images klas�r�n�n i�inde
//scriptler scripts klas�r�n�n i�inde
//sesler sound klas�r�n�n i�inde
// sahneler scene klas�r�n�n i�inde
// oyuna ba�lamak i�in scene klas�r� i�inde main menu sahnesini se�ip play butonuna bas�n�z.

Oyunun yap�l���

// �nce projemin konusunu belirledim.

// Konusunu belirledikten sonra projem i�in gerekli assetleri internetten buldum.

// Assetleri photoshop program� kullanarak projem i�in haz�r hale getirdim.

// Unity5.1 (2D)projemin i�ine onlar� drag edip �zerinde de�i�iklik yapt���mda yada tekrar kullanmak istedi�imde rahatl�k olsun diye;
   bana gerekli olacak assetleri prefab haline getirdim.

// Projem i�in gerekli animasyonlar� haz�rlad�m(buldu�um resimler animasyon i�in yeterliydi).
   Animasyonlar i�in gerekli resimleri ��yle haz�rlad�m;
   -sprite mode k�sm�ndan multiple se�ene�ini i�aretledim.
   -Sprite Editor kullanarak resimleri kestim
   -Resimlerin oynamamas� i�in kesme k�sm�ndaki noktay� bir yere sabitledim.
   Animasyon resimlerini haz�rlad�ktan sonra animasyonumu olu�turup animation klas�r�nde toplad�m hepsini.
   Animator penceresini kullanarak state lerini haz�rlad�m. Baz� animasyonlarda (asans�r kolu gibi) loop se�mesini kapad�m ve
   animasyonun �al���p �al��mad��� durumunu tutucak bir bool cinsinden parametre tan�mlad�m.

// Level lar�m�  haz�rlad�m . Kulland���m asset'lere birbirlerini tan�yabilmeleri i�in Polygon collider2d ekledim.
   Kolayl�k olsun diye , karakterimin �arpt��� zaman tan�mas� gereken objelere, isimleriyle teker teker bulup kontrol etmek zor olucak diye
   tag olu�turdum(ate� , su , d��me , asans�r kolu). Canvas kullanarak k�rm�z� ve mavi sincab�n puanlar�n� tutacak iki tane image yaratt�m 
   ve i�lerine puanlar� g�sterebilmeleri i�in text koydum. ayn� �ekilde zaman� g�stermek i�inde bi tane image yarat�p i�ine �ocu�u olarak text yaratt�m

// Level'lar haz�r olduktan sonra kodlar�m� yazmaya ba�lad�m.

// �lk �nce oyunu y�netmesi i�in game manager(GM) kodu haz�rlad�m;

// GM kodu i�inde;
     -parametreler;
	- int K�rm�z�Puan : k�rm�z� sincab�n toplad��� puan� tutmak i�in.
	- int MaviPuan : Mavi sincab�n toplad��� puan� tutmak i�in.
	- bool PauseGame : oyunun duraklat�l�p duraklat�lmad��� durumunu tutmak i�in.
	- bool EnterBlue : mavi sincab�n mavi kap�ya girip girmedi�i durumunu tutmas� i�in.
	- bool EnterRed : k�rm�z� sincab�n k�rm�za kap�ya girip girmedi�i durumunu tutmas� i�in. 
	- int GameTime : her level 60 saniyede tamamlanmak zorunda bu saniyeyi tutmas� i�in.
     -fonksiyonlar;
	-void ContinueButton ()-> Ara men� sayfas�nda continue butonuna bas�ld���nda �a��r�lan fonksiyon
				  Zaman akmaya ba�lar, Pausegame=false olur , 
	  			  Ekrana ��kan ara men� ve butonlar� kapat�r.
	-void SetRedTime ()-> k�rm�z� sincap level'i ba�ar�yla tamamland��� zaman puan�na kalan s�re eklenir. 
	-void SetBlueTime ()-> mavi sincap level'i ba�ar�yla tamamland��� zaman puan�na kalan s�re eklenir. 
	-void AddBluePoint () -> mavi sincap mavi elmaslar� toplad��� zaman her toplad��� elmas i�in puan�na 10 puan eklenir. 
	-void AddK�rm�z�Point () -> k�rm�z� sincap k�rm�z� elmaslar� toplad��� zaman her toplad��� elmas i�in puan�na 10 puan eklenir.
	-void Update () -> 
			   * Gametime 0 ise GameTime'� oyunun zaman�na e�itledim ��nk� level'de ba�ar�s�z oldu�un zaman tekrar oynad���nda 
			     zaman bi �nceki kald��� yerden devam ediyordu.
			   * Ge�en s�re 60 oldu�u zaman , GameTime'� 0 'a e�itleyip (tekrar y�klendi�inde zaman kald��� yerden devam etmesin diye)
			     Gameover sahnesini �al��t�rd�m.  
			   * EnterRed ve EnterBlue true oldu�u zaman yani iki sincapta kap�dan i�eri girdi�i zaman bir sonraki level'� �al��t�rd�m.
			   * Escape tu�una bas�ld���nda oyun duraksamada de�ilse , zaman� duraksatt�m pausegame=true yapt�m ve ara men� ve butonlar�n� g�r�n�r yapt�m.
			   * Escape tu�una bas�ld���nda oyun duraksamadaysa , zaman� ba�latt�m ve  pausegame=false yapt�m ve ara men� ve butonlar�n g�r�n�rl���n� kald�rd�m.
			   * zaman ve sincaplar�n puanlar�n� g�stermesi i�in yaratt���m image'lar�n i�indeki text'leri buldurup i�ine g�sterecekleri de�erleri atad�m.

// Controller kodu i�inde;
     -parametreler;
	-bool boundry : z�plama s�resi.
	- bool jumped :  z�play�p z�plamad���n� tutmak i�in.
	- GM gm : controller script'inden GM script'ine ula�mak i�in.
	- Color x : sincaplar ate�e veya suya de�di�inde ��kan bulut animasyonunun ekrandan yava� yava� silinmesi i�in.
	- Animator anim : Animator'a ula�mak i�in.
	- bool dead : sincaplar�n �l�p �lmedi�ini tutmas� i�in. 
	- float speed : sincaplar�n h�zlar�n� tutmak i�in.

     -fonksiyonlar ;
	-void OnCollisionStay2D()-> sincap ta�a ��kt��� zaman sincab� ta��n �ocu�u  yap�yo ki ta�la beraber yukar� ��ks�n.	
	-void OnCollisionExit2D()-> sincap ta�tan ayr�ld��� zaman, sincab�n art�k ta��n �ocu�u olmamas�n� sa�l�yor.
   	-void OnTriggerEnter2D() -> *e�er sincap anahtara de�erse anahtar yok oluyo kap�y� tan�mas� i�in 
				     kap�n�n collider � �al���r hale getiriliyor ve kap�n�n animasyonu �al��t�r�l�yor.
				    *sincaplar elmaslara de�di�i zaman elmas yok oluyor ve GM script'indeki addRedPoint() fonksiyonu �a��r�l�p puan ekleniyor
				    *sincap e�er kap�dan ge�erse yok oluyor ve GM kodundaki setRedTime fonksiyonu �a��r�l�p son puan� hesaplan�yor.
				    *sincap ate�e veya suya de�erse sincab�n sprite � kald�r�l�p yerine patlama bulutunun sprite � koyulup patlama animasyonu ba�lat�l�yor.
				     sonra dead=true yap�l�yor ve SpriteAlfaKapa() coroutine fonksiyonu �a��r�l�yor.
	- IEnumarator SpriteAlfaKapa()-> ekranda ��kan patlama bulutunun renginin alfas�n� yava� yava� kapatarak ekranda yava� yava� kaybolmas�n� sa�l�yor ve gameover sahnesini a��yor.
	- void Start() -> parametrelerin ilk de�erleri atan�yor.
	- void Movement() -> e�er klavyede sa�tu�a bas�l�yorsa sa�a gitme , sol tu�a bas�l�yorsa sola gitmesini sa�l�yor ve bunlar�n animasyonlar�n� �al��t�r�yor.
			     yukar� tu�una bas�l�rsa z�plamay� sa�l�yor(z�plama esnas�nda bir daha z�plamas�n diye jumped ile z�play�p z�plamad��� kontrol ediliyor)
 
// Asansor kodu i�inde(bu kod asans�r� �al��t�racak objelerin(d��meler ve asans�r kollar� ) i�indedir);
      -parametreler;
	-Vector3 position1 , position2 : ta��n nerden nereye gidece�ini tutar.
	-Gameobject Asansor : Hangi ta��n hareket edece�ini s�ylemek ve hareket ettirmek i�in.
      -fonksiyonlar;
	-void awake() -> parametrelerin ilk de�erleri atan�yor.
	-void OntriggerStay() -> sincaplarla temas halinde oldu�u s�rece;
				 script hangi objenin i�indeyse onun animasyonu �al���yor ve sincaplar�n ge�ebilmeleri i�in ta� hareket ettiriliyor.
	-void OnTriggerExit() -> sincaplarla temas bitti�inde animasyon durdurulup ta� yerine geri d�nd�r�l�yor.

// UI kodu i�inde(UI kodu level'lar�n �al��ma d�zenini d�zenlemek i�in yaz�lm��t�r.) ;
       -parametreler: 
	-Gm gm : bu scripten gm'e ula�abilmek i�in.
       -fonksiyonlar:
	-void start() -> e�er �uanda main men�de yada gameover da de�ilsek gm in de�eri atan�yor.
	-void PlayButton()-> play button ' a bas�ld���nda bu fonksiyon �a��r�l�p bir sonraki level y�kleniyor.
	-void ContinueButton()-> Continue button'a bas�ld���nda gm'deki ContinueButton() fonksiyonu �a��r�l�yor.
	-void QuitMainMenuButton()-> Main men�ye d�nmeyi sa�l�yor.
	-void QuitButton() -> oyundan ��kmay� sa�l�yor.
