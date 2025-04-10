# BalloonPop
BalloonPop est un jeu mobile hyper casual développé avec Unity, dans lequel vous incarnez un ballon s’élevant vers le ciel dans un décor semé d’embûches.

## 🚀 Gameplay
Le ballon se gonfle automatiquement. En maintenant le doigt sur l’écran :
  - Le ballon se dégonfle, ce qui réduit sa taille et augmente sa vitesse verticale.
  - La position du toucher sur l’écran influe sur la direction du ballon :
    - En dessous du ballon : il se dégonfle et monte plus rapidement.
    - À droite du ballon : il s’incline vers la droite et part vers la gauche.
    - À gauche du ballon : il s’incline vers la gauche et part vers la droite.

## ⚠️ Obstacles à éviter
  - Fils électriques : mortels si on les touche.
  - Tuyaux : certains nécessitent d’être petit pour passer, d'autres vous propulsent à la sortie.
  - Ventilateurs : vous repoussent selon leur position.
  - Oiseaux : perchés sur les fils, ils lâchent de la fiente à intervalles réguliers.
  - Fiente d’oiseau :  elle vous fait exploser.
  - Blocage : si le ballon reste coincé trop longtemps sans monter, il explose.

## 🎮 Contrôles
Appui/maintien sur l’écran : dégonfle le ballon et influe sur sa direction selon la position du doigt.

## 🛠️ Fonctionnalités
Physique réaliste via Rigidbody.
Système de gonflement/dégonflement dynamique : influence la vitesse, la taille et les interactions.
Object Pooling pour les projectiles de fiente (pas de Instantiate/Destroy inutiles).
