# Chatman
Chatman est une application de messagerie en ligne pour Windows développée entièrement en C#. Pour stocker et récupérer ses données, l'application communique avec un serveur REST distant grâce à la librairie Flurl.

Dans un premier temps, un utilisateur peut se connecter ou créer un compte pour accéder aux écrans principaux de l'application, cette gestion des utilisateurs se fait par simple couple nom d'utilisateur/mot de passe. A la connexion, un token garantissant l'authenticité de l'utilisateur est récupéré.

Une fois connecté à l'application, l'utilisateur sera présenté à sa liste de contacts. Ici, plusieurs actions sont possibles :
- Ajouter un nouveau contact en cliquant sur le boutton '+' en bas à gauche de l'écran. L'utilisateur sera ensuite redirigé vers un écran où il pourra ajouter un nouvel utilisateur à ses contacts graçe à son nom d'utilisateur
- Retirer un contact de sa liste en cliquant sur le boutton '-' en bas à gauche de l'écran
- Afficher le chat avec l'utilisateur sélectionné en cliquant sur son nom dans la liste des contacts
- Envoyer un message au contact sélectionné dans la barre de message 

La récupération des nouveaux messages et contacts se fait automatiquement toutes les 5 secondes et met à jour le contenu de l'application : A la réception d'un nouveau message le chat est mis à jour et si ces deux utilisateurs ne s'étaient jamais envoyés de messages auparavant, le contact ainsi que le chat sont créés.

Dans un chat, les messages reçus s'affichent sur le côté gauche de l'écran en noir et les messages envoyés s'affichent sur le côté droit en vert.


---


Installation
-

- Dans Android Studio, importez la solution du projet et lancez là
- L'application s'ouvre et peut-être exploitée

---

Projet M2 MBDS Nice Sophia Antipolis - Promo 2018/2019 - Enseignant: Alexandre Maisonobe - Langage : C#
