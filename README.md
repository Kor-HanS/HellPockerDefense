# HellPockerDefense
구현내용
(새로 알게 된 것) 은 거의 유튜브 영상 참조 함.
https://www.youtube.com/watch?v=gH7zsOmGW0Q&list=PLC2Tit6NyVicvqMTDJl8e-2IB4v_I7ddd&index=8

1. Ray / RayCastHit 을 이용한 타일 및 타워 인식 (Objectdetecter.cs) (새로 알게 된 것)
1-1. 타워 중복 생성 방지(Tile.cs)
1-2. 타워 정보 출력(TowerVier.cs)

2. 라운드 시작 / 라운드 종료 / 타워 생성 시간 / 포커 패 세팅 전반적인 게임 흐름 구현.

3. UI 세팅(라운드 / 적의 수 / 타워 정보 / 타이머 / 카드 패) 구현.

4. 포커 카드 패 / 카드 패 타워 생성 시간에 세팅(Card.cs / PockerGenerater.cs)

5. 타워 공격 구현(TowerWeapon.cs) (새로 알게 된 것)
상태 패턴을 통해서 코루틴 함수 이용

6. (Enemy.cs) / (Tower.cs) 적 및 타워 클래스 구현

7. (EnemySpawner.cs) / (TowerSpawner.cs) 적 스포너 및 타워 스포너 클래스 구현.
코루틴 함수로 spawnTime 줘서 적 스폰 시키는것. (새로 알게 된 것)

8. 타일 및 타일 팔레트를 통한 타워디펜스 맵 구현
타일 팔레트로 타일 그리기 / RuleTile 만들기 (새로 알게 된 것) 
GameObjectBrush 를 통한 WayPoints 그리기 -> Enemy.cs 에서 Setup함수로 wayPoints 배열 받고 타일 이동시키기. (새로 알게 된 것)

9. (GameManager.cs) 싱글톤 패턴으로 구현시키기. -> 디자인 패턴 책 보고 만들어 보려고 했음.
   잘 된것인지도 잘 모르겟음. 씬이 한개 밖에 없는 게임이라. 
   결론적으로, 런 타임내내 인스턴스 1개 유지되고 있고, GameManager내 public static 변수로 본인을 인스턴스로 받고, 다른 클래스에서 접근 가능하게 만듬.
9-1. 게임 매니저내 내부 클래스 WaitForClass 구현 -> 코루틴 함수 yieldInstruction new 연산 최적화.
   
10. 투사체 (Projectile.cs) -> Setup 함수를 타워 component 에서 카드패에 따라 다르게 가지고 있는 여러 필드중 데미지 받아서 씀.

(미 구현)

11. 적 Hp 게이지 구현. Slider Ui

12. 적을 죽이면 얻는 돈으로 타워 powerup 구현 및 돈이 없는데, 적 흘리면 게임 종료.

23 - 01 - 25
![image](https://user-images.githubusercontent.com/99121615/213970803-e37a96ae-ce48-40ac-b77a-676a9ce32727.png)

23 - 01 - 29
![image](https://user-images.githubusercontent.com/99121615/215318783-8f554347-2e97-4bfd-a6ac-6e5ebf821c9f.png)
