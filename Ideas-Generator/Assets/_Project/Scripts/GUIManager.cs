using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gisha.IdGen
{
    public class GUIManager : MonoBehaviour
    {
        [Header("Listing Instantiating")]
        [SerializeField] private GameObject _wordListingPrefab;
        [SerializeField] private Transform _wordListingParent;
        [Header("Other")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_Text[] _cardTexts;

        List<TMP_Text> _wordListings = new List<TMP_Text>();

        public void OnClick_Add()
        {
            if (string.IsNullOrWhiteSpace(_inputField.text))
                return;

            var listing = Instantiate(_wordListingPrefab, _wordListingParent);

            var listingText = listing.GetComponent<TMP_Text>();
            listingText.text = _inputField.text;
            _wordListings.Add(listingText);

            // Clear input field after adding a new listing.
            _inputField.text = string.Empty;
        }

        public void OnClick_ClearAll()
        {
            foreach (TMP_Text listing in _wordListings)
                Destroy(listing.gameObject);

            _wordListings = new List<TMP_Text>();
        }

        public void OnClick_Shuffle()
        {
            List<TMP_Text> variants = new List<TMP_Text>(_wordListings);

            if (variants.Count < 3)
            {
                Debug.LogError("Not enough words to shuffle.");
                return;
            }

            foreach (var card in _cardTexts)
            {
                TMP_Text variant = variants[Random.Range(0, variants.Count)];
                card.text = variant.text;
                variants.Remove(variant);
            }
        }
    }
}
