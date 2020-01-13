﻿using System;
using System.Linq;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;

namespace AutomaticSpeechRecognition
{
    internal class GrammarFactory
    {
        private readonly GrammarBuilder _systemGrammarBuilder = new GrammarBuilder(new Choices(Info.DefaultCommands.ToArray()));
        private readonly GrammarBuilder _choicesGrammarBuilder = new GrammarBuilder(new Choices(Info.PizzaChoices.ToArray()));
        private readonly GrammarBuilder _cakesGrammarBuilder = new GrammarBuilder(new Choices(Info.Cakes.ToArray()));
        private readonly GrammarBuilder _dipsGrammarBuilder = new GrammarBuilder(new Choices(Info.Dipps.ToArray()));

        public void BuildGrammars(SpeechRecognitionEngine engine)
        {
            BuildSingleGrammars(engine);
            BuildDoubleGrammars(engine);
            BuildTripleGrammars(engine);

        }

        public Grammar BuildXmlGrammar(string grammarFilePath)
        {
            var grammarDoc = new SrgsDocument(grammarFilePath);
            return new Grammar(grammarDoc);
        }
        private void BuildSingleGrammars(SpeechRecognitionEngine engine)
        {
            Console.WriteLine(engine.RecognizerInfo.Culture);
            _systemGrammarBuilder.Culture = engine.RecognizerInfo.Culture;
            var systemGrammar = new Grammar(_systemGrammarBuilder);

            _choicesGrammarBuilder.Culture = engine.RecognizerInfo.Culture;
            _cakesGrammarBuilder.Culture = engine.RecognizerInfo.Culture;
            _dipsGrammarBuilder.Culture = engine.RecognizerInfo.Culture;

            var choiceGrammar = new Grammar(_choicesGrammarBuilder);
            var cakeGrammar = new Grammar(_cakesGrammarBuilder);
            var dipGrammar = new Grammar(_dipsGrammarBuilder);
            engine.LoadGrammar(choiceGrammar);
            engine.LoadGrammar(cakeGrammar);
            engine.LoadGrammar(dipGrammar);
            engine.LoadGrammar(systemGrammar);
        }

        private void BuildDoubleGrammars(SpeechRecognitionEngine engine)
        {
            var builder = new GrammarBuilder();
            builder.Append(_choicesGrammarBuilder);
            builder.Append(_cakesGrammarBuilder);

            var grammar = new Grammar(builder);
            engine.LoadGrammar(grammar);


            builder = new GrammarBuilder();
            builder.Append(_choicesGrammarBuilder);
            builder.Append(_dipsGrammarBuilder);

            grammar = new Grammar(builder);
            engine.LoadGrammar(grammar);


            builder = new GrammarBuilder();
            builder.Append(_cakesGrammarBuilder);
            builder.Append(_choicesGrammarBuilder);
            grammar = new Grammar(builder);

            engine.LoadGrammar(grammar);

        }

        private void BuildTripleGrammars(SpeechRecognitionEngine engine)
        {
            var builder = new GrammarBuilder();
            builder.Append(_choicesGrammarBuilder);
            builder.Append(_cakesGrammarBuilder);
            builder.Append(_dipsGrammarBuilder);
            builder.Culture = engine.RecognizerInfo.Culture;

            var grammar = new Grammar(builder);

            engine.LoadGrammar(grammar);

            builder = new GrammarBuilder();
            builder.Append(_choicesGrammarBuilder);
            builder.Append(_dipsGrammarBuilder);
            builder.Append(_cakesGrammarBuilder);
            builder.Culture = engine.RecognizerInfo.Culture;
            grammar = new Grammar(builder);

            engine.LoadGrammar(grammar);


            builder = new GrammarBuilder();
            builder.Append(_cakesGrammarBuilder);
            builder.Append(_choicesGrammarBuilder);
            builder.Append(_dipsGrammarBuilder);
            builder.Culture = engine.RecognizerInfo.Culture;
            grammar = new Grammar(builder);

            engine.LoadGrammar(grammar);

        }
    }
}
