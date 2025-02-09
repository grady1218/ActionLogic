﻿using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool IsSearching { get; set; } = false;
	public bool IsClear { get; set; } = false;
	public bool IsMenuOpen { get; set; } = false;
	[NonSerializedAttribute] public AudioSource AudioSource;
	private bool _isPushBox = false;

	[SerializeField] private GameObject MyCamera;
	[SerializeField] private AudioClip StepSound;

	private Vector3 _retryPos;
	private GameObject _particle;

	// Start is called before the first frame update
	void Start()
	{
		Application.targetFrameRate = 60;
		_retryPos = transform.position;
		_particle = transform.GetChild(0).gameObject;
		AudioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		var inputValue = Input.GetAxisRaw("Horizontal");

		if (IsSearching || IsClear || IsMenuOpen)
		{
			inputValue = 0;
		}
		//  エフェクト再生
		if (inputValue != 0)
		{
			if (!AudioSource.isPlaying) AudioSource.PlayOneShot(StepSound);
			var e = _particle.GetComponent<ParticleSystem>().emission;
			e.enabled = true;
		}
		else if (inputValue == 0)
		{
			if (AudioSource.isPlaying) AudioSource.Stop();
			var e = _particle.GetComponent<ParticleSystem>().emission;
			e.enabled = false;
		}
		// プレイヤー操作
		transform.position += new Vector3(inputValue * 2 * Time.deltaTime, 0);

		// 反対方向を向いているのなら画像を反転する
		if (inputValue == -1) GetComponent<SpriteRenderer>().flipX = true;
		else if (inputValue == 1) GetComponent<SpriteRenderer>().flipX = false;

		// 箱を押さない状態で歩いているならアニメーションを再生する
		if (!_isPushBox) GetComponent<Animator>().SetFloat("MoveCount", Mathf.Abs(inputValue));

	}
}
