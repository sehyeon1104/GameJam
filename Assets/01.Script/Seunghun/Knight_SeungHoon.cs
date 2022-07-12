using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_SeungHoon : MonoBehaviour
{
    private Transform playerTransform;
    private float blockRadius = 1f;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        StartCoroutine(KnightM());
    }

    private IEnumerator KnightM()
    {
        while (transform.position != playerTransform.position)
        {
            yield return new WaitForSeconds(Sync_Gijoo.Instance.tikTime);
            transform.position += new Vector3((playerTransform.position.x - transform.position.x) > 0 ? 1 : (playerTransform.position.x - transform.position.x) < 0 ? -1 : 0 * blockRadius, (playerTransform.position.y - transform.position.y) > 0 ? 1 : (playerTransform.position.y - transform.position.y) < 0 ? -1 : 0 * blockRadius);
            //�÷��̾� ��ġ(3,3) - �� ��ġ(0,0)�� ��, ���� (1,1)��ŭ �̵��ؾߵ�
            //�׷��ϱ� �÷��̾� ��ġ�� X��ǥ - �� ��ġ�� X��ǥ�� 0���� ũ�� 1��ŭ �̵�, 0�̸� �̵� ����, 0���� ������ -1��ŭ �̵�
            //
            yield return new WaitForSeconds(Sync_Gijoo.Instance.tikTime);
            switch (Random.Range(0, 2))
            {
                case 0:
                    if(playerTransform.position.y - transform.position.y == 0)
                        transform.position += new Vector3(((playerTransform.position.x - transform.position.x) > 0 ? 1 : -1) * blockRadius, blockRadius);
                    else
                        transform.position += new Vector3(blockRadius, (playerTransform.position.y - transform.position.y) > 0 ? 1 : -1 * blockRadius);
                    break;
                case 1:
                    if (playerTransform.position.y - transform.position.y == 0)
                        transform.position += new Vector3((playerTransform.position.x - transform.position.x) > 0 ? 1 : -1 * blockRadius, -blockRadius);
                    else
                        transform.position += new Vector3(-blockRadius, (playerTransform.position.y - transform.position.y) > 0 ? 1 : -1 * blockRadius);
                    break;
            }
            yield return new WaitForSeconds(Sync_Gijoo.Instance.tikTime);
            transform.position += new Vector3((playerTransform.position.x - transform.position.x) > 0 ? 1 : (playerTransform.position.x - transform.position.x) < 0 ? -1 : 0 * blockRadius, (playerTransform.position.y - transform.position.y) > 0 ? 1 : (playerTransform.position.y - transform.position.y) < 0 ? -1 : 0 * blockRadius);
        }
    }
}