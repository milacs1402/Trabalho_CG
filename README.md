# 🛵 EuComo

Um jogo de simulação de entregas estilo arcade, focado em gestão de tempo, navegação urbana e reflexos rápidos. Desenvolvido na **Unity**.

## 🎮 Sobre o Jogo
O jogador assume o papel de um entregador que precisa correr contra o tempo para bater a meta financeira do dia. O gameplay alterna entre **navegação em mundo aberto** (pilotagem) e **minigames de entrega** (ação rápida).

## ✨ Funcionalidades Principais
* **Sistema de GPS Dinâmico:** Rotas traçadas em tempo real usando NavMesh e LineRenderer.
* **Trânsito Inteligente:** Carros controlados por IA (NavMesh Agents) que circulam pela cidade.
* **Minigame de "Urgência":** Mecânica de *button mashing* com integração de vídeo e pausa temporal (Unscaled Time).
* **Economia Variável:** O valor da gorjeta depende da performance do jogador no minigame (Mais rápido = Mais dinheiro).
* **Sistema de Vitória/Derrota:** Meta financeira acumulativa vs. Cronômetro regressivo.

## 🕹️ Controles

| **Acelerar / Frear** | `W` / `S`  |
| **Virar** | `A` / `D` |
| **Drift** | `Espaço` |
| **Realizar Entrega (Minigame)** | `Espaço` (Pressionar Repetidamente) |

## 🛠️ Tecnologias Utilizadas
* **Engine:** Unity 3D
* **Linguagem:** C#
* **Sistemas:** Unity NavMesh, UI System, Video Player API.

## 🚀 Como Rodar o Projeto
1.  Clone este repositório.
2.  Abra o **Unity Hub**.
3.  Adicione a pasta do projeto e abra.
4.  Abra a cena `MenuInicial`.
5.  Dê Play!

---
Desenvolvido por **Camila Cristina e Davi Bandeca**.
