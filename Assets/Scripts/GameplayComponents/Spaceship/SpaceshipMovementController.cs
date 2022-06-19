using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovementController : MonoBehaviour,IMovementController
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D spaceShipRigidBody;
    [SerializeField] float maxSpeed;
    [SerializeField] float thrust;
    [SerializeField] float rotationSpeed;
    IGameBoard gameBoard;
    float movementUpAxis;
    bool isEnabled=true;

    private void FixedUpdate()
    {
        if (movementUpAxis != 0f && isEnabled)
            MoveShip();
    }

    private void MoveShip()
    {
        spaceShipRigidBody.AddForce(transform.up * thrust * movementUpAxis);
        spaceShipRigidBody.velocity = Vector2.ClampMagnitude(spaceShipRigidBody.velocity,maxSpeed);
        CheckIfPositionIsValid(transform.position);
    }


    public void Move(IMoveCommand moveCommand)
    {
        movementUpAxis = moveCommand.MovementYAxis;
    }

    public void Rotate(IMoveCommand moveCommand)
    {
        if (!isEnabled)
            return;


        var rotationAxis = rotationSpeed * moveCommand.RotationAxis*Time.deltaTime;
        transform.Rotate(0, 0, rotationAxis, Space.World);
    }

    public void SetGameBoard(IGameBoard board)
    {
        gameBoard = board;
    }

    public void CheckIfPositionIsValid(Vector3 position)
    {
        if (position.x > gameBoard.HorizotalCameraBoundary + 5f)
        {
            transform.position = new Vector3(-gameBoard.HorizotalCameraBoundary - 1, transform.position.y, 0);
        }
        else if (position.x < -gameBoard.HorizotalCameraBoundary - 5f)
        {
            transform.position = new Vector3(gameBoard.HorizotalCameraBoundary + 1, transform.position.y, 0);
        }
        else if (position.y > gameBoard.VerticallCameraBoundary + 5f)
        {
            transform.position = new Vector3(transform.position.x, -gameBoard.VerticallCameraBoundary - 1, 0);
        }
        else if (position.y < -gameBoard.VerticallCameraBoundary - 5f )
        {
            transform.position = new Vector3(transform.position.x, gameBoard.VerticallCameraBoundary+1, 0);
        }
       
    }

    public void EnableComponent(bool enable)
    {
        isEnabled = enable;
        if (!isEnabled)
        { 
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            spaceShipRigidBody.velocity = Vector2.zero;
        }
    }
}
