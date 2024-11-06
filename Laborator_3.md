  Laborator 3

*1.* Care este ordinea de desenare a vertexurilor pentru aceste metode (*orar* sau *anti-orar*)?
Vertexurile sunt desenate în ordinea în care sunt specificate.În general anti-orar, indicând partea frontală a obiectului.  
*2.* Ce este *anti-aliasing*? Prezentați această tehnică pe scurt.
*Anti-aliasing* este o tehnică folosită pentru  netezirea marginilor zimțate ale obiectelor digitale.
*3.* Care este efectul rulării comenzii *GL.LineWidth(float)*? Dar pentru *GL.PointSize(float)*? Funcționează în interiorul unei zone GL.Begin()?
*GL.LineWidth(float)* pentru  grosimea liniilor, *GL.PointSize(float)* pentru dimensiunea punctelor și nu funcționează  în interiorul unei zone *GL.Begin()*.
*4.* Răspundeți la următoarele întrebări (utilizați ca referință eventual și tutorii OpenGL Nate Robbins):
* Care este efectul utilizării directivei *LineLoop* atunci când desenate segmente de dreaptă multiple în OpenGL?
 *LineLoop* conectează un set de puncte într-o buclă închisă și  legă primul vertex de ultimul vertex.
* Care este efectul utilizării directivei *LineStrip* atunci când desenate segmente de dreaptă multiple în OpenGL?
*LineStrip* pentru n puncte formează n linii.
* Care este efectul utilizării directivei *TriangleFan* atunci când desenate segmente de dreaptă multiple în OpenGL?
 *TriangleFan* face ca primul varf să fie fixat
*TriangleFan* creează un evantai de triunghiuri care pornesc de la un punct central.
* Care este efectul utilizării directivei *TriangleStrip* atunci când  se desenează segmente de dreaptă multiple în OpenGL?
*TriangleStrip* creează un triunghi din alte 3 triunghiuri adiacente.
*6.* Urmăriți aplicația „shapes.exe” din tutorii OpenGL Nate Robbins. De ce este importantă utilizarea de culori diferite (în gradient sau culori selectate per suprafață) în desenarea obiectelor 3D? Care este avantajul?
Utilizarea culorilor diferite este importantă fiindcă  ajută la distingerea obiectelor, a detaliilor, astfel oferind o vizualizare mai bună a ansamblului.
*7.* Ce reprezintă un gradient de culoare? Cum se obține acesta în OpenGL?
 *Gradientul de culoare* reprezintă  tranziția dintre două sau mai multe  culori.
*10.* Ce efect are utilizarea unei culori diferite pentru fiecare vertex atunci când desenați o linie sau un triunghi în modul strip?
Dacă vertexurile au culori diferite se va forma un gradient din acestea.

