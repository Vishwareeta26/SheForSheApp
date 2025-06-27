using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button LitButton;
    public Button SciButton;
    public Button MalButton;
    public Button PolButton;

    [Header("Info UI")]
    public Text InfoText;

    [Header("Prefabs")]
    public GameObject IconLiteraturePrefab;
    public GameObject IconStemPrefab;
    public GameObject IconPeacemakerPrefab;
    public GameObject IconMalaysianPrefab;

    [Header("Image Target (Placeholder Cube)")]
    public Transform cube;

    private GameObject currentInstance;
    private GameObject currentPrefabType;

    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();

    // New data structure
    private Dictionary<string, string[]> infoDictionary = new Dictionary<string, string[]>()
    {
        {
            "literature", new string[]
            {
                "Jane Austen's background: Jane Austen was an English novelist born in the 18th century, best known for her keen observations of romantic relationships and social dynamics. She grew up in a large family and was largely educated at home. Though she lived a quiet life, her insights into class and gender have resonated across generations.",
                "Jane Austen's notable works: Her six major novels include Pride and Prejudice, Sense and Sensibility, and Emma, which critique societal expectations through clever heroines and witty narration. These works remain widely read, adapted into countless films and series. Austen’s novels subtly advocate for women’s independence within a rigidly structured society.",
                "Jane Austen's quote: “There is no charm equal to tenderness of heart.” This quote encapsulates her enduring belief in emotional integrity and kindness over superficial charm.",
                "Jane Austen's fun fact: During her lifetime, her books were published anonymously, credited only to 'A Lady.' She never married, though her novels often focused on marriage as a theme. Jane Austen appears today on the UK £10 note — a rare honor for a woman from her time.",
                "Virginia Woolf's background: Virginia Woolf was an English modernist writer and literary innovator in the early 20th century. She is widely regarded as a central figure in feminist literary criticism and experimental fiction. Her life was deeply affected by mental health struggles, which influenced much of her work.",
                "Virginia Woolf's notable works: Her major works include Mrs. Dalloway, a novel exploring a single day in the life of a woman; To the Lighthouse, a reflective family narrative; and the feminist essay A Room of One’s Own, arguing for women's intellectual freedom. Her pioneering use of stream-of-consciousness narration reshaped modern fiction. Woolf’s work has become a cornerstone in both literary and gender studies.",
                "Virginia Woolf's quote: “No need to hurry. No need to sparkle. No need to be anybody but oneself.” This quote reflects her belief in authenticity and inner reflection over societal expectation.",
                "Virginia Woolf's fun fact: Virginia Woolf often wrote while standing at her tall writing desk, believing it helped her focus. She was also a founding member of the Bloomsbury Group, a collective of intellectuals and artists. Despite the era's taboos, Woolf was open about same-sex attraction and explored it in her fiction.",
                "Agatha Christie's background: Dame Agatha Christie (1890–1976), the 'Queen of Crime,' was a British mystery novelist and playwright whose works have sold over two billion copies and been translated into more than 100 languages",
                "Agatha Christie's notable works: She created iconic detectives Hercule Poirot and Miss Marple, appearing in 66 detective novels and 19 plays. Her best-known works include The Mysterious Affair at Styles (1920), The Murder of Roger Ackroyd (1926), and the record-breaking play The Mousetrap (running since 1952)",
                "Agatha Christie's : “My chief dislikes are crowds, loud noises… I do like sun, sea, flowers…” — Christie, describing her preferences and personality",
                "Agatha Christie's fun fact: She once disappeared for 11 days in 1926, sparking a national manhunt before being found in a hotel under an assumed name; she also wrote six romantic novels under the pseudonym Mary Westmacott",
                "Mary Shelley's background: Mary Wollstonecraft Shelley (1797–1851) was an English novelist and editor, best known for writing Frankenstein at age 18 during a ghost-story challenge at Lake Geneva. Daughter of feminist Mary Wollstonecraft and philosopher William Godwin, she experienced loss early—her mother died days after giving birth—then eloped with Percy Shelley and faced family tragedy while writing her masterpiece ",
                "Mary Shelley's notable works: Frankenstein; or, The Modern Prometheus (1818), often considered the first science-fiction novel, was inspired by her nightmare and debates on electricity and galvanism. She also authored novels like Valperga, The Last Man, and Lodore, and edited her husband's works",
                "Mary Shelley's quote: “What terrified me will terrify others…”—from her 1831 introduction, describing how a waking dream inspired Frankenstein ",
                "Mary Shelley's fun fact: She published Frankenstein anonymously in 1818; her name appeared only in later editions. She survived smallpox, endured personal tragedies including the death of three children and her husband Percy Bysshe Shelley, and later became a pioneering feminist literary figure"
            }
        },
        {
            "stem", new string[]
            {
                "Marie Curie's background: Marie Curie was a physicist and chemist of Polish origin who became a naturalized French citizen. She was the first woman to win a Nobel Prize and remains the only person awarded Nobel Prizes in two different sciences — Physics and Chemistry. Her research laid the foundation for the study of radioactivity, a term she coined.",
                "Marie Curie's notable works: Curie discovered the elements polonium and radium, and conducted groundbreaking research on radioactive substances. She shared the 1903 Nobel Prize in Physics with her husband Pierre Curie and later received the 1911 Nobel Prize in Chemistry. Her work revolutionized cancer treatment and nuclear physics.",
                "Marie Curie's quote: “Nothing in life is to be feared, it is only to be understood.” This speaks to her fearless curiosity and commitment to scientific progress in the face of adversity.",
                "Marie Curie's fun fact: Her notebooks and personal items are still so radioactive that they’re stored in lead-lined boxes and require protective gear to handle. Curie refused to patent her radium-isolation process, believing scientific knowledge should benefit all of humanity. She also served on the front lines during World War I with mobile X-ray units she helped develop.",
                "Grace Hopper's background: Grace Hopper was an American computer scientist and naval officer who was instrumental in the early development of programming languages. She held a Ph.D. in mathematics and became one of the first programmers of the Harvard Mark I computer during World War II. Known for her bold thinking and wit, she broke barriers for women in tech and the military.",
                "Grace Hopper's notable works: Hopper developed the first compiler for a computer programming language, which led to the creation of COBOL — a language still used today. She was a pioneer in making computers more accessible by advocating for English-like programming syntax. Her innovations greatly influenced the direction of modern computing.",
                "Grace Hopper's quote: “The most dangerous phrase in the language is, ‘We’ve always done it this way.’” A call to challenge norms, this quote embodies her commitment to innovation and progress.",
                "Grace Hopper's fun fact: The term “debugging” originates from an incident where Hopper removed a moth from a malfunctioning computer. She was posthumously awarded the Presidential Medal of Freedom in 2016. Known for her humility, she often carried around nanoseconds (wires cut to the length light travels in a nanosecond) to teach about computer processing time.",
                "Margaret Hamilton's background: Margaret Hamilton is an American computer scientist and systems engineer who led development of the Apollo Guidance Computer software at MIT in the 1960s. Her pioneering work in error‑detection, software reliability, and asynchronous programming laid the foundation for modern software engineering ",
                "Margaret Hamilton's notable works: Hamilton led the team that developed onboard flight software for Apollo missions, managing critical alarms during Apollo 11’s lunar landing. She also coined 'software engineering' and founded Hamilton Technologies Inc. to continue her work ",
                "Margaret Hamilton's quote: “When I first got into it, nobody knew what it was that we were doing. It was like the Wild West.” Reflecting the nascent, experimental nature of early software development ",
                "Margaret Hamilton's fun fact: Her Apollo code printouts once formed a stack taller than she was; she received the NASA Exceptional Space Act Award, and a minor planet, LEGO set, and popular culture references now honor her legacy",
                "Katherine Bouman's background: Katherine (Katie) Bouman is an American computer scientist and engineer, born in 1989. She earned her BS at the University of Michigan and her SM and PhD at MIT, where she researched computational imaging. As a grad student, she led the development of CHIRP for the Event Horizon Telescope and is now a professor at Caltech.",
                "Katherine Bouman's notable works: Bouman spearheaded CHIRP (Continuous High-resolution Image Reconstruction using Patch priors), an algorithm essential for processing the raw EHT telescope data. She played a central role in the team—including ~200 scientists—that produced the first image of the M87 black hole in April 2019 ",
                "Katherine Bouman's quote: “No one algorithm or person made this image…it required the amazing talent of a team of scientists from around the globe and years of hard work.” — Katie Bouman, emphasizing collaborative effort ",
                "Katherine Bouman's fun fact: Her reaction photo went viral as she watched the image form; in 2021 she was honored with the Royal Photographic Society’s Progress Medal for her work in imaging black holes "
            }

        },
        {
            "peacemaker", new string[]
            {
                "Jacinda Ardern's background: Jacinda Ardern served as the 40th Prime Minister of New Zealand, gaining international respect for her empathetic and pragmatic leadership. She became the world's youngest female head of government at age 37. Her leadership style emphasized kindness, inclusivity, and swift crisis response.",
                "Jacinda Ardern's notable works: Ardern led New Zealand through major events including the COVID-19 pandemic and the 2019 Christchurch mosque shootings, where her compassionate response gained global admiration. Her policies focused on child poverty, mental health, and climate change. She was known for effectively communicating with the public, often using social media and clear messaging.",
                "Jacinda Ardern's quote: “One of the criticisms I’ve faced over the years is that I’m not aggressive enough... I'm trying to chart a different way.” Her quote redefines leadership as strength through empathy, not dominance.",
                "Jacinda Ardern's fun fact: Ardern became only the second world leader to give birth while in office, balancing motherhood and governance. She once worked at a fish and chip shop and served as a DJ before politics. In 2023, she voluntarily stepped down, saying she no longer felt she could do the job justice — a rare move in global politics.",
                "Sanna Marin's background: Sanna Marin is a Finnish politician who became the Prime Minister of Finland in 2019 at just 34, making her one of the youngest leaders in the world at the time. Raised by a single mother in a same-sex household, her rise to power is seen as a symbol of social progress. She leads with a strong focus on equality, sustainability, and digital innovation.",
                "Sanna Marin's notable works: Marin has been a strong advocate for climate action and labor reforms and has led Finland during key moments like the COVID-19 pandemic and NATO membership application. Her government prioritized education, mental health, and transparent governance. She is recognized for bringing fresh energy to European politics.",
                "Sanna Marin's quote: “I represent a younger generation. I also represent the voice of equality.” Marin’s words reflect her role as a modern, progressive leader for the 21st century.",
                "Sanna Marin's fun fact: Before entering politics, she worked as a cashier and was the first in her family to attend university. She gained viral attention for being photographed in leather jackets and boots, challenging stereotypes of political formality. Marin is known for maintaining a relatable presence on social media, especially among young voters.",
                "Greta Thunberg's background: Greta Thunberg is a Swedish climate activist who rose to global prominence at age 15 after staging weekly school strikes for climate outside the Swedish Parliament in 2018, sparking the Fridays for Future movement ",
                "Greta Thunberg's notable works: She co-authored books including No One Is Too Small to Make a Difference and The Climate Book (2022), and made notable UN speeches such as her 'How dare you?' address at COP24",
                "Greta Thunberg's quote: “You have stolen my dreams and my childhood with your empty words.” — a searing rebuke to world leaders, widely circulated from her UN and media speeches ",
                "Greta Thunberg's fun fact: Diagnosed with Asperger’s, she calls it a 'superpower'; she once sailed across the Atlantic in a zero-emissions yacht to avoid flights and refuses most awards, donating prize money to environmental causes ",
                "Malala Yousafzai's background: Malala Yousafzai (b. 1997, Swat, Pakistan) is a Nobel Peace Prize–winning education activist who began writing for BBC Urdu at age 11 to document life under Taliban rule ",
                "Malala Yousafzai's notable works: She authored I Am Malala and Malala’s Magic Pencil, co-founded the Malala Fund in 2013, and became the youngest-ever Nobel Prize laureate in 2014 for her advocacy of girls’ education ",
                "Malala Yousafzai's quote: “I raise my voice not so that I can shout, but so that those without a voice can be heard.” — a line highlighting her commitment to amplifying silenced voices ",
                "Malala Yousafzai's fun fact: She survived a Taliban assassination attempt at age 15, later earning honorary Canadian and U.S. citizenships, and was appointed the UN’s youngest Messenger of Peace in 2017 "
            }
        },
        {
            "malaysian", new string[]
            {
                "Dato’ Sri Siti Nurhaliza's background: Dato’ Sri Siti Nurhaliza is a celebrated Malaysian singer and entrepreneur recognized for her powerful vocals and lasting influence on Southeast Asian pop culture. She debuted at age 16 and has since garnered numerous national and regional music awards while launching successful beauty and wellness ventures",
                "Dato’ Sri Siti Nurhaliza's notable works: Her signature songs include 'Cindai', 'Aku Cinta Padamu', and 'Biarlah Rahsia'; she has released over 17 studio albums and served as a cultural ambassador in tours across Asia ",
                "Dato’ Sri Siti Nurhaliza's quote: “Populariti... bukan terletak pada gaya seksi,” — emphasizing that true popularity isn’t based on superficial appeal",
                "Dato’ Sri Siti Nurhaliza's fun fact: In 2007, her likeness was featured as a virtual agent in Malaysia’s Windows Live Messenger campaign to promote tourism to Japan ",
                "Datuk Nicol Ann David's background: Datuk Nicol Ann David (born 1983) is widely considered the greatest women’s squash player ever, dominating the world rankings for a record 108 consecutive months from 2006 to 2015 ",
                "Datuk Nicol Ann David's notable works: She won the World Open title eight times and claimed the British Open five times, plus an unmatched nine Asian Squash Championships and multiple WISPA Player of the Year awards ",
                "Datuk Nicol Ann David's fun fact: Nicol was invited to carry Malaysia’s Olympic torch in 2004 and was honored with the Order of Merit by the King in 2008 for her achievements ",
                "Dr. Anita Yusof's background: Dr. Anita Yusof is the first Malaysian Muslim woman to circumnavigate the globe solo on a motorcycle, covering over 65,000 km across 40 countries during her 2015–16 'Global Dream Ride' ",
                "Dr. Anita Yusof's notable works: She completed the ride on a Yamaha FZ150i, earned a place in the Asian Book of Records, and later served as a GIVI ambassador while lecturing in sports science",
                "Dr. Anita Yusof's quote: “Some people even apologized for being prejudice against Islam and Muslims,” she said after her journey changed perceptions",
                "Dr. Anita Yusof's fun fact: Along her travels she braved extreme conditions like 52 °C sandstorms and even water poisoning, and she once rode solo across Afghanistan’s challenging terrain ",
                "Tun Fatimah Hashim's background: Tun Fatimah Hashim (1924–2010) was Malaysia’s first female Cabinet minister and a pioneering advocate for women’s rights and education after independence .",
                "Tun Fatimah Hashim's notable works: She led UMNO’s women’s wing (Kaum Ibu/Wanita UMNO) for 16 years, became Welfare Minister (1969–1973), co-founded the National Council of Women’s Organisations, and started Malaysia’s first Women’s Day celebrations in 1962 ",
                "Tun Fatimah Hashim's quote: “What drove me was realisation ... They’re as qualified and educated but did not have an equal position. I could not ignore the issue and had to fight for the cause.” ",
                "Tun Fatimah Hashim's fun fact: She was honored as Pro-Chancellor of UTM and posthumously had a leadership centre and gallery at UKM named after her; she’s also the only woman buried at Malaysia’s Makam Pahlawan "
            }
        }
    };

    private int currentIndex = 0; // Track current index for looping
    private string currentKey; // Track current key

    void Start()
    {
        Debug.Log("TaskManager started.");
        LitButton.onClick.AddListener(() => ReplacePrefab(IconLiteraturePrefab, "literature"));
        SciButton.onClick.AddListener(() => ReplacePrefab(IconStemPrefab, "stem"));
        PolButton.onClick.AddListener(() => ReplacePrefab(IconPeacemakerPrefab, "peacemaker"));
        MalButton.onClick.AddListener(() => ReplacePrefab(IconMalaysianPrefab, "malaysian"));

        // Ensure no prefab is shown initially
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }

        // Subscribe to screen tap
        Input.touches[0].tapCount = 1; // This is for demonstration; implement actual tap detection
    }

    // void Update()
    // {
    //     // Check for screen tap (this is a simple check, adjust as needed)
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         UpdateInfoBoard();
    //     }
    // }
    void Update()
{
    // Check for screen swipe
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        
        // Check if the touch phase is ended to detect a swipe
        if (touch.phase == TouchPhase.Ended)
        {
            UpdateInfoBoard();
        }
    }
}

    void ReplacePrefab(GameObject newPrefab, string key)
    {
        if (newPrefab == null || cube == null)
        {
            Debug.LogWarning("New prefab or cube is null.");
            return;
        }

        Debug.Log("Attempting to replace with prefab: " + newPrefab.name);

        if (currentPrefabType == newPrefab)
        {
            Debug.Log("Same prefab already shown. Skipping.");
            return;
        }

        if (currentInstance != null)
        {
            ReturnToPool(currentInstance);
        }

        currentInstance = GetFromPool(newPrefab);
        currentInstance.transform.SetParent(cube, true);
        currentPrefabType = newPrefab;
        currentInstance.SetActive(true);

        currentKey = key; // Set current key for info board
        currentIndex = 0; // Reset index
        UpdateInfoBoard();

        Debug.Log("New prefab instantiated: " + currentInstance.name);
    }

    GameObject GetFromPool(GameObject prefab)
    {
        if (!pool.ContainsKey(prefab))
        {
            pool[prefab] = new Queue<GameObject>();
        }

        if (pool[prefab].Count > 0)
        {
            GameObject instance = pool[prefab].Dequeue();
            instance.SetActive(true);
            return instance;
        }
        else
        {
            return Instantiate(prefab);
        }
    }

    void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
        if (pool.ContainsKey(currentPrefabType))
        {
            pool[currentPrefabType].Enqueue(instance);
        }
        else
        {
            Destroy(instance);
        }
    }

    void UpdateInfoBoard()
    {
        if (currentKey != null && infoDictionary.ContainsKey(currentKey))
        {
            var info = infoDictionary[currentKey];

            // Update text based on current index
            InfoText.text = info[currentIndex];

            // Increment index and loop back if necessary
            currentIndex++;
            if (currentIndex >= info.Length)
            {
                currentIndex = 0; // Reset index if at the end
            }
        }
    }

}

// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class TaskManager : MonoBehaviour
// {
//     [Header("UI Buttons")]
//     public Button LitButton;
//     public Button SciButton;

//     [Header("Info UI")]
//     public Text InfoText;

//     [Header("Prefabs")]
//     public GameObject IconVirginiaPrefab;
//     public GameObject IconJanePrefab;
//     public GameObject IconMariePrefab;

//     [Header("Image Target (Placeholder Cube)")]
//     public Transform cube;

//     private GameObject currentInstance;
//     private GameObject currentPrefabType;

//     private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();

//     private Dictionary<string, string[]> infoDictionary = new Dictionary<string, string[]>
//     {
//         {
//             "jane", new string[]
//             {
//                 "background: Jane Austen was an English novelist born in the 18th century, best known for her keen observations of romantic relationships and social dynamics. She grew up in a large family and was largely educated at home. Though she lived a quiet life, her insights into class and gender have resonated across generations.",
//                 "notable works: Her six major novels include Pride and Prejudice, Sense and Sensibility, and Emma, which critique societal expectations through clever heroines and witty narration. These works remain widely read, adapted into countless films and series. Austen’s novels subtly advocate for women’s independence within a rigidly structured society.",
//                 "quote: “There is no charm equal to tenderness of heart.” This quote encapsulates her enduring belief in emotional integrity and kindness over superficial charm.",
//                 "fun fact: During her lifetime, her books were published anonymously, credited only to 'A Lady.' She never married, though her novels often focused on marriage as a theme. Jane Austen appears today on the UK £10 note — a rare honor for a woman from her time."
//             }
//         },
//         {
//             "virginia", new string[]
//             {
//                 "background: Virginia Woolf was an English modernist writer and literary innovator in the early 20th century. She is widely regarded as a central figure in feminist literary criticism and experimental fiction. Her life was deeply affected by mental health struggles, which influenced much of her work.",
//                 "notable works: Her major works include Mrs. Dalloway, a novel exploring a single day in the life of a woman; To the Lighthouse, a reflective family narrative; and the feminist essay A Room of One’s Own, arguing for women's intellectual freedom. Her pioneering use of stream-of-consciousness narration reshaped modern fiction. Woolf’s work has become a cornerstone in both literary and gender studies.",
//                 "quote: “No need to hurry. No need to sparkle. No need to be anybody but oneself.” This quote reflects her belief in authenticity and inner reflection over societal expectation.",
//                 "fun fact: Virginia Woolf often wrote while standing at her tall writing desk, believing it helped her focus. She was also a founding member of the Bloomsbury Group, a collective of intellectuals and artists. Despite the era's taboos, Woolf was open about same-sex attraction and explored it in her fiction."
//             }
//         },
//         {
//             "marie", new string[]
//             {
//                 "background: Marie Curie was a physicist and chemist of Polish origin who became a naturalized French citizen. She was the first woman to win a Nobel Prize and remains the only person awarded Nobel Prizes in two different sciences — Physics and Chemistry. Her research laid the foundation for the study of radioactivity, a term she coined.",
//                 "notable works: Curie discovered the elements polonium and radium, and conducted groundbreaking research on radioactive substances. She shared the 1903 Nobel Prize in Physics with her husband Pierre Curie and later received the 1911 Nobel Prize in Chemistry. Her work revolutionized cancer treatment and nuclear physics.",
//                 "quote: “Nothing in life is to be feared, it is only to be understood.” This speaks to her fearless curiosity and commitment to scientific progress in the face of adversity.",
//                 "fun fact: Her notebooks and personal items are still so radioactive that they’re stored in lead-lined boxes and require protective gear to handle. Curie refused to patent her radium-isolation process, believing scientific knowledge should benefit all of humanity. She also served on the front lines during World War I with mobile X-ray units she helped develop."
//             }
//         }
//     };

//     private string currentKey;
//     private int currentInfoIndex = 0;

//     // For swipe detection
//     private Vector2 swipeStartPos;
//     private bool isSwiping = false;

//     // Literature prefab rotation
//     private List<GameObject> literaturePrefabs;
//     private List<string> literatureKeys;
//     private int currentLitIndex = 0;

//     void Start()
//     {
//         // Initialize prefab/key lists
//         literaturePrefabs = new List<GameObject>() { IconVirginiaPrefab, IconJanePrefab };
//         literatureKeys = new List<string>() { "virginia", "jane" };

//         LitButton.onClick.AddListener(() =>
//         {
//             currentLitIndex = 0;
//             ReplacePrefab(literaturePrefabs[currentLitIndex], literatureKeys[currentLitIndex]);
//         });

//         SciButton.onClick.AddListener(() => ReplacePrefab(IconMariePrefab, "marie"));

//         if (currentInstance != null)
//         {
//             Destroy(currentInstance);
//         }
//     }

//     void Update()
//     {
//         DetectSwipe();
//     }

//     void DetectSwipe()
//     {
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);
//             switch (touch.phase)
//             {
//                 case TouchPhase.Began:
//                     swipeStartPos = touch.position;
//                     isSwiping = true;
//                     break;

//                 case TouchPhase.Ended:
//                     if (!isSwiping) return;

//                     float swipeDelta = touch.position.x - swipeStartPos.x;
//                     if (Mathf.Abs(swipeDelta) > 50f)
//                     {
//                         if (currentKey == "virginia" || currentKey == "jane")
//                         {
//                             if (swipeDelta > 0)
//                                 ShowPreviousLiteraturePrefab();
//                             else
//                                 ShowNextLiteraturePrefab();
//                         }
//                         else
//                         {
//                             UpdateInfoBoard(); // For non-swipe categories like science
//                         }
//                     }

//                     isSwiping = false;
//                     break;
//             }
//         }
//     }

//     void ShowNextLiteraturePrefab()
//     {
//         currentLitIndex = (currentLitIndex + 1) % literaturePrefabs.Count;
//         ReplacePrefab(literaturePrefabs[currentLitIndex], literatureKeys[currentLitIndex]);
//     }

//     void ShowPreviousLiteraturePrefab()
//     {
//         currentLitIndex = (currentLitIndex - 1 + literaturePrefabs.Count) % literaturePrefabs.Count;
//         ReplacePrefab(literaturePrefabs[currentLitIndex], literatureKeys[currentLitIndex]);
//     }

//     void ReplacePrefab(GameObject newPrefab, string key)
//     {
//         if (newPrefab == null || cube == null) return;

//         if (currentInstance != null)
//         {
//             ReturnToPool(currentInstance);
//         }

//         currentInstance = GetFromPool(newPrefab);
//         currentInstance.transform.SetParent(cube, true);
//         currentInstance.transform.localPosition = Vector3.zero;
//         currentInstance.transform.localRotation = Quaternion.identity;
//         currentPrefabType = newPrefab;
//         currentInstance.SetActive(true);

//         currentKey = key;
//         currentInfoIndex = 0;
//         UpdateInfoBoard();
//     }

//     GameObject GetFromPool(GameObject prefab)
//     {
//         if (!pool.ContainsKey(prefab))
//             pool[prefab] = new Queue<GameObject>();

//         if (pool[prefab].Count > 0)
//         {
//             var instance = pool[prefab].Dequeue();
//             instance.SetActive(true);
//             return instance;
//         }

//         return Instantiate(prefab);
//     }

//     void ReturnToPool(GameObject instance)
//     {
//         instance.SetActive(false);
//         if (!pool.ContainsKey(currentPrefabType))
//             pool[currentPrefabType] = new Queue<GameObject>();

//         pool[currentPrefabType].Enqueue(instance);
//     }

//     void UpdateInfoBoard()
//     {
//         if (!string.IsNullOrEmpty(currentKey) && infoDictionary.ContainsKey(currentKey))
//         {
//             var info = infoDictionary[currentKey];
//             InfoText.text = info[currentInfoIndex];
//             currentInfoIndex = (currentInfoIndex + 1) % info.Length;
//         }
//     }
// }
