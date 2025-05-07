To configure collation for your operation, create an instance of the
`Collation <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Collation.html>`__ class.

The following table describes the parameters that the ``Collation`` constructor accepts.
It also lists the corresponding class property that you can use to read each
setting's value.

.. list-table::
   :header-rows: 1
   :widths: 20 60 20

   * - Parameter
     - Description
     - Class Property

   * - ``locale``
     - | Specifies the International Components for Unicode (ICU) locale. For a list of
         supported locales,
         see :manual:`Collation Locales and Default Parameters </reference/collation-locales-defaults/#supported-languages-and-locales>`
         in the {+mdb-server+} Manual.
       |
       | If you want to use simple binary comparison, use the ``Collation.Simple`` static
         property to return a ``Collation`` object with the ``locale`` set to ``"simple"``.
       | **Data Type**: {+string-data-type+} 
     - ``Locale`` 

   * - ``alternate``
     - | Optional. Specifies whether the driver considers whitespace and punctuation as base
         characters for purposes of comparison.
       |
       | **Data Type**: `CollationAlternate <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.CollationAlternate.html>`__
       | **Default**: ``CollationAlternate.NonIgnorable`` (spaces and punctuation are
         considered base characters)
     - ``Alternate``

   * - ``backwards``
     - | Optional. Specifies whether strings containing diacritics sort from the back of the string
         to the front.
       |
       | **Data Type**: {+bool-data-type+} 
       | **Default**: ``false``
     - ``Backwards``

   * - ``caseFirst``
     - | Optional. Specifies the sort order of case differences during tertiary level comparisons.
       |
       | **Data Type**: `CollationCaseFirst <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.CollationCaseFirst.html>`__
       | **Default**: ``CollationCaseFirst.Off``
     - ``CaseFirst``

   * - ``caseLevel``
     - | Optional. Specifies whether to include case comparison at strength level ``Primary`` or
         ``Secondary``. 
       |
       | When this argument is ``true``:
       |
       | - If the strength is ``Primary``, the driver compares base
           characters and case.
       | - If the strength is ``Secondary``, the driver compares base
           characters, diacritics, other secondary differences, and case.
       |
       | When this argument is ``false``, the driver doesn't include case comparison at
         strength level ``Primary`` or ``Secondary``.
       |
       | **Data Type**: {+bool-data-type+} 
       | **Default**: ``false``
     - ``CaseLevel``

   * - ``maxVariable``
     - | Optional. Specifies which characters the driver considers ignorable when
         ``Alternate`` is ``CollationAlternate.Shifted``.
       |
       | **Data Type**: `CollationMaxVariable <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.CollationMaxVariable.html>`__ 
       | **Default**: ``CollationMaxVariable.Punctuation`` (the driver ignores punctuation
         and spaces)
     - ``MaxVariable``
   
   * - ``normalization``
     - | Optional. Specifies whether the driver normalizes text as needed.
       |
       | Most text doesn't require normalization.
       |
       | **Data Type**: {+bool-data-type+}
       | **Default**: ``false``
     - ``Normalization``

   * - ``numericOrdering``
     - | Optional. Specifies whether the driver compares numeric strings as numbers.
       | 
       | If this argument is ``true``, the driver compares numeric strings as numbers.
         For example, "10" is greater than "2". If this argument is ``false`` or excluded,
         the driver compares numeric strings as strings. For example, "10" is less than "2".
       |
       | For more information, see :manual:`Collation Restrictions </reference/collation/#restrictions>`
         in the {+mdb-server+} manual.
       |
       | **Data Type**: {+bool-data-type+}
       | **Default**: ``false``
     - ``NumericOrdering``

   * - ``strength``
     - | Optional. Specifies the level of comparison to perform, as defined in the
         `ICU documentation <https://unicode-org.github.io/icu/userguide/collation/concepts.html#comparison-levels>`__.
       |
       | **Data Type**: `CollationStrength <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.CollationStrength.html>`__
       | **Default**: ``CollationStrength.Tertiary``
     - ``Strength``

For more information about collation, see the :manual:`Collation <reference/collation>`
page in the {+mdb-server+} manual.