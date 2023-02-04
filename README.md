# HellPockerDefense
구현내용
(새로 알게 된 것) 은 거의 유튜브 영상 참조 함.
https://www.youtube.com/watch?v=gH7zsOmGW0Q&list=PLC2Tit6NyVicvqMTDJl8e-2IB4v_I7ddd&index=8

1. Ray / RayCastHit 을 이용한 타일 및 타워 인식 (Objectdetecter.cs) (새로 알게 된 것)
-타워 중복 생성 방지(Tile.cs)
-타워 정보 출력(TowerVier.cs)

2. 라운드 시작 / 라운드 종료 / 타워 생성 시간 / 포커 패 세팅 전반적인 게임 흐름 구현.

3. UI 세팅(라운드 / 적의 수 / 타워 정보 / 타이머 / 카드 패) 구현.

4. 포커 카드 패 / 카드 패 타워 생성 시간에 세팅(Card.cs / PockerGenerater.cs)

5. 타워 공격 구현(TowerWeapon.cs) (새로 알게 된 것)
상태 패턴을 통해서 코루틴 함수 이용

6. (Enemy.cs) / (Tower.cs) 적 및 타워 클래스 구현

7. (EnemySpawner.cs) / (TowerSpawner.cs) 적 스포너 및 타워 스포너 클래스 구현.
코루틴 함수로 spawnTime 줘서 적 스폰 시키는것. (새로 알게 된 것)

8. 타일 및 타일 팔레트를 통한 타워디펜스 맵 구현
-타일 팔레트로 타일 그리기 / RuleTile 만들기 (새로 알게 된 것) 
-GameObjectBrush 를 통한 WayPoints 그리기 -> Enemy.cs 에서 Setup함수로 wayPoints 배열 받고 타일 이동시키기. (새로 알게 된 것)

9. (GameManager.cs) 싱글톤 패턴으로 구현시키기. -> 디자인 패턴 책 보고 만들어 보려고 했음.
   잘 된것인지도 잘 모르겟음. 씬이 한개 밖에 없는 게임이라. 
   결론적으로, 런 타임내내 인스턴스 1개 유지되고 있고, GameManager내 public static 변수로 본인을 인스턴스로 받고, 다른 클래스에서 접근 가능하게 만듬.  
-게임 매니저내 내부 클래스 WaitForClass 구현 -> 코루틴 함수 yieldInstruction new 연산 최적화.
   
10. 투사체 (Projectile.cs) -> Setup 함수를 타워 component 에서 카드패에 따라 다르게 가지고 있는 여러 필드중 데미지 받아서 씀.

11. 적 Hp 게이지 구현. Slider Ui 
- UI 오브젝트들. hpBar 같은것들 캔버스 밑에 자식으로 있지 않으면 화면상에 안나옴. (새로 알게 된 것) 
- Slider 컴포넌트 value 값 을 통해 적의 남은 Hp 화면상에서 확인 가능. (새로 알게 된 것) 
- Enemy.cs에 화면에 올라오는 hpBar 오브젝트 및 적의 maxHp/currentHp 필드로 추가
- EnemySpawer.cs 에서 적의 체력바 리스트 필드로 추가 및 관리. Enemy 오브젝트 생성시, 체력바도 같이 생성하고, Enemy.cs에 Setup 함수로 값 주기. 

12. 타워 업그레이드 수단 이자, 목숨인 돈 UI 추가. -> 돈이 0보다 적어질시, 게임 종료 게임종료 로직 추가.
- 게임종료 조건시, 게임 종료 씬으로 전환.

13. 시스템 메세지 추가.(알파값 으로 text 보여주고 안보여주기.) (새로 알게 된 것) 
- 타워 사격 범위 localScale 값 바꾸는거 -> 절대적 크기로 바꿈. parent null로 주고. localScale 바꾸고 다시 parent 붙이기.
- 라운드 시작 / 타워 생성 시간 시스템 메세지 출력

14. 투사체 에셋 변경. 
에셋 출처 : https://pimen.itch.io/fire-spell

(미 구현)

15. 타워 업그레이드 구현.(투사체 데미지 : 타워 레벨 x 타워 카드패 데미지)

23 - 01 - 25
![image](https://user-images.githubusercontent.com/99121615/213970803-e37a96ae-ce48-40ac-b77a-676a9ce32727.png)

23 - 01 - 29
![image](https://user-images.githubusercontent.com/99121615/215318783-8f554347-2e97-4bfd-a6ac-6e5ebf821c9f.png)

23 - 02 - 03
![image](https://user-images.githubusercontent.com/99121615/216518600-32a58b09-d6be-43eb-91e7-5085e4db692f.png)
![image](https://user-images.githubusercontent.com/99121615/216518635-795254f7-fef3-4068-a848-ee84e8fa18ed.png)



