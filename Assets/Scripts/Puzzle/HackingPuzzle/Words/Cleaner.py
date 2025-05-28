f = open("Words.txt", 'r')
words = f.readlines()[0].split()
clean_words = []
for w in words:
    if len(w) < 4:
        continue
    clean_words.append(w)
f = open("CleanedWords.txt", 'w')
f.write(' '.join(clean_words))