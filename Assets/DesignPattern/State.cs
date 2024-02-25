/****************************************************
 *              디자인 패턴 State                   *
 ****************************************************/

/*
 * <상태 패턴>
 * 객체에게 한 번에 하나의 상태만을 가지게 하며
 * 객체는 현재 상태에 해당하는 행동만을 진행함
 * 
 * <구현>
 * 1. 열거형 자료형으로 객체가 가질 수 있는 사태들을 정의
 * 2. 현재 상태를 저장하는 변수에 초기 상태를 지정
 * 3. 객체는 행동에 있어서 현재 상태만의 행동을 진행
 * 4. 객체는 현재 상태의 행동을 진행 한 후 상태 변화에 대해 판단
 * 5. 상태 변화가 있어야 하는 경우 현재 상태를 대상 상태로 지정
 * 6. 상태가 변경된 경우 다음 행동에 있어서 바뀐 상태만의 행동을 진행
 * 
 * <장점>
 * 1. 조건문을 상태로 처리가 가능하므로, 복잡한 조건처리에 대한 부담이 적음
 * 2. 현재 상태에 대한 연산만 처리하므로, 연산속도가 뛰어남
 * 3. 동작의 구현을 각각의 상태를 분산시키므로, 코드가 간결하고 가독성이 좋음
 * 
 * <주의점>
 * 1. 상태의 구분이 명확하지 않거나 갯수가 많은 경우, 상태 코드가 복잡해질 수 있음
 */

using UnityEngine;

namespace DesignPattern
{
    public class Mobile
    {
        public enum State { Off, Normal, Charge, FullCharged }

        private State state = State.Normal;
        private bool charging = false;
        private float battery = 50.0f;

        private void Update()
        {
            switch (state)
            {
                case State.Off:
                    OffUpdate();
                    break;
                case State.Normal:
                    NormalUpdate();
                    break;
                case State.Charge:
                    ChargeUpdate();
                    break;
                case State.FullCharged:
                    FullChargedUpdate();
                    break;
            }
        }

        private void OffUpdate()
        {
            // Off work
            // Do nothing

            if (charging)
            {
                state = State.Charge;
            }
        }

        private void NormalUpdate()
        {
            // Normal work
            battery -= 1.5f * Time.deltaTime;

            if (charging)
            {
                state = State.Charge;
            }
            else if (battery <= 0)
            {
                state = State.Off;
            }
        }

        private void ChargeUpdate()
        {
            // Charge work
            battery += 25f * Time.deltaTime;

            if (!charging)
            {
                state = State.Normal;
            }
            else if (battery >= 100)
            {
                state = State.FullCharged;
            }
        }

        private void FullChargedUpdate()
        {
            // FullCharged work
            // Do nothing

            if (!charging)
            {
                state = State.Normal;
            }
        }

        public void ConnectCharger()
        {
            charging = true;
        }

        public void DisConnectCharger()
        {
            charging = false;
        }
    }
}