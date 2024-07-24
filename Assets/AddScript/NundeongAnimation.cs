using UnityEngine;

public class NundeongAnimation : MonoBehaviour
{
    public Transform nundeong; // ������ Transform
    public Transform arCamera; // AR ī�޶� Transform
    public float moveDuration = 2f; // �̵� �ִϸ��̼� ���� �ð�
    public float floatAmplitude = 0.1f; // ���ִ� �ִϸ��̼��� ����
    public float floatFrequency = 1f; // ���ִ� �ִϸ��̼��� �ֱ�

    private Vector3 startPos;
    private Vector3 endPos;
    private float elapsedTime = 0f;
    private bool isMoving = true;
    private Vector3 floatStartPos;

    void Start()
    {
        // ���� ��ġ�� �� ��ġ ����
        startPos = arCamera.TransformPoint(new Vector3(-1.5f, 1.5f, 5f)); // AR ī�޶��� ���� ��� (ȭ�� ��ǥ��)
        endPos = arCamera.TransformPoint(new Vector3(0f, 0f, 2f)); // AR ī�޶��� ���� (ȭ�� �߾�)

        //��ġ�� �� �����ϰ� �ʹٸ� x, y, z ���� �����Ͽ� ���ϴ� ��ġ�� ����
        //���� ��ġ�� �� ���� ���� �̵���Ű�� �ʹٸ� x ���� �� �۰� �ϰ�, y ���� �� ũ�� ����
        //���� ��ġ�� �� �߾ӿ� ���߰� �ʹٸ� x�� y ���� 0�� ������ �����ϸ� ��.

        // ������ �ʱ� ��ġ ����
        nundeong.position = startPos;

        // �ִϸ��̼� ����
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            nundeong.position = Vector3.Lerp(startPos, endPos, t);

            if (t >= 1f)
            {
                isMoving = false;
                floatStartPos = nundeong.position;
                elapsedTime = 0f; // ���ִ� �ִϸ��̼��� ���� �ð� �ʱ�ȭ
            }
        }
        else
        {
            // �սǵս� ���ִ� �ִϸ��̼�
            float yOffset = floatAmplitude * Mathf.Sin(floatFrequency * elapsedTime * Mathf.PI * 2);
            nundeong.position = floatStartPos + new Vector3(0, yOffset, 0);
            elapsedTime += Time.deltaTime;
        }
    }
}