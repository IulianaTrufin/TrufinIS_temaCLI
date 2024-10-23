**1.Ce este un viewport?**_
Viewport-ul este regiunea în care este desenat conținutul grafic generat de OpenGL.

**2.Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?**
Conceptul de frames per seconds definește numărul de cadre randate și afișate într-o secundă.

**3.Când este rulată metoda OnUpdateFrame()?**
Metoda OnUpdateFrame() este rulată pentru fiecare cadru nou.

**_4.Ce este modul imediat de randare?_**
Modul imediat de randare în OpenGL permite desenarea obiectelor prin indicarea vertecșilor direct, utilizând comenzi precum glBegin() și glEnd(). Este ușor de folosit, dar nu este eficient pentru scene complexe și a fost înlocuit cu metode mai moderne.

**5.Care este ultima versiune de OpenGL care acceptă modul imediat?**
Ultima versiune care acceptă modul imediat este OpenGL 3.1.

**6,Când este rulată metoda OnRenderFrame()?**  
metoda OnRenderFrame() este rulată înaintea afișării unui nou cadru pe ecran.

**7.De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?** 
Metoda OnResize() trebuie să fie executată cel puțin o dată, fiincă astfel se ajunstează dimensiunile viewport-ului și se asigura o afișare corectă după redimensionarea ferestrei.

**8.Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?**
Metoda CreatePerspectiveFieldOfView() are următorii parametri care configurează proiecția 3D a scenei:

**fovy** (_Field Of View_):  Unghiul câmpului vizual în direcția y ,
**aspect** (_Aspect Ratio_): Reprezintă raportul dintre lățimea și înălțimea viewport-ului,
**zNear** (_Near Plane Distance_): Distanța față de planul apropiat,
**zFar** (_Far Plane Distance_): Distanța până la planul îndepărtat.
